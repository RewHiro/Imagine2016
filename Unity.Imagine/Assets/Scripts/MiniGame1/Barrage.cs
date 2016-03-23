using UnityEngine;
using System.Collections;

public class Barrage : ActionManager
{
    [SerializeField]
    AudioPlayer _audioPlayer;

    [SerializeField]
    StartCount _startCount;

    [SerializeField]
    GameObject _bulletObj = null;

    [SerializeField]
    GameObject _specialBulletObj = null;

    [SerializeField]
    TimeCount _timeCount = null;

    [SerializeField]
    int _probability = 0;

    [SerializeField]
    float _waitTime = 0.1f;

    private enum SelectPlayer
    {
        Player1,
        Player2
    }

    private GameObject _enemy;

    private int _keyCount = 0;

    int _count = 0;

    RandomBullet _randomBullet;

    public int _getKeyCount { get { return _keyCount; } }

    CharacterData _characterData;

    public GameObject getCustomGameObject { get { return _characterData.gameObject; } }

    void Start()
    {


        if (_audioPlayer == null)
        {
            _audioPlayer = GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>();
        }

        if (_startCount == null)
        {
            _startCount = GameObject.Find("StartCount").GetComponent<StartCount>();
        }

        if (_timeCount == null)
        {
            _timeCount = GameObject.Find("Time").GetComponent<TimeCount>();
        }

        _randomBullet = GetComponent<RandomBullet>();

        _characterData = GetComponentInChildren<CharacterData>();

    }

    void Update(){}

    int Barragebutton(KeyCode key)
    {
        if (_timeCount._getTime <= 1) return 0;
        if (Input.GetKeyDown(key))
        {
            _audioPlayer.Play(20, false);
            StartCoroutine(BulletCreate(_waitTime));
            return 1;
        }

        return 0;
    }

    IEnumerator BulletCreate(float waitTime)
    {
        if (_timeCount._getTime <= 1) yield break;
        Bullet(_bulletObj);
        _count++;
        if (_probability > _count) yield break;
        if (_randomBullet.StatusRandomBullet() == false) yield break;
        _count = 0;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("来てる");
        Bullet(_specialBulletObj);
        _keyCount++;
        yield return 0;
    }


    public override void Action()
    {
        Vector3 myRotate = transform.eulerAngles;
        transform.LookAt(Enemy.transform);
        rotation = transform.eulerAngles;
        transform.eulerAngles = myRotate;

        if (_startCount.getCountFinish)
        {
            _keyCount += Barragebutton(keyCode);
        }
    }

    public void Bullet(GameObject bullet)
    {
        _enemy = Enemy.GetComponentInChildren<CharacterData>().gameObject;
        var obj = Instantiate(bullet);
        obj.name = bullet.name;
        obj.transform.position = transform.position;
        var value = _enemy.transform.position - transform.position;
        obj.GetComponent<BulletShot>()._vectorValue = value.normalized;
        obj.GetComponent<BulletShot>()._parent = _enemy;
    }

}
