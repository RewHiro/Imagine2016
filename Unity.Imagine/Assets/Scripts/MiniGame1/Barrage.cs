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

    //[SerializeField]
    private int _keyCount = 0;

    [SerializeField]
    TimeCount _timeCount = null;

    [SerializeField]
    int _probability = 0;

    //[SerializeField]
    //ActiveModel _enemyActiveModel;

    private enum SelectPlayer
    {
        Player1,
        Player2
    }

    //[SerializeField]
    //private SelectPlayer _selectPlayer;


    private GameObject _enemy;


    [SerializeField]
    float _waitTime = 0.1f;


    int _count = 0;

    RandomBullet _randomBullet;

    public int _getKeyCount { get { return _keyCount; } }

    CharacterData _characterData;

    public GameObject getCustomGameObject { get { return _characterData.gameObject; } }


    void Awake()
    {
    //    _modelParameterInfo = GetComponentInChildren<ModelParameterInfo>();
     //   Debug.Log(_modelParameterInfo.name);

    }

    void Start()
    {

        
    //    _enemy = _selectPlayer == SelectPlayer.Player1 ?
    //GameObject.Find("Player2") : GameObject.Find("Player1");

        //_enemyActiveModel = _enemy.GetComponent<ActiveModel>();

        if(_audioPlayer == null)
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

        
        //Debug.Log(_enemy);
    }

    void Update()
    {
        if (_startCount.getCountFinish)
        {
            _keyCount += Barragebutton(keyCode);
        }
    }


    int Barragebutton(KeyCode key)
    {
        if (_timeCount._getTime <= 0) return 0;
        if (Input.GetKeyDown(key))
        {
            _audioPlayer.Play(20,false);
             StartCoroutine(BulletCreate( _waitTime));
            return 1;
        }

        return 0;
    }


    IEnumerator BulletCreate(float waitTime)
    {
        if (_timeCount._getTime <= 0) yield break;
        Bullet(_bulletObj);
        _count++;
        if (_probability > _count) yield break;
        if (_randomBullet.StatusRandomBullet() == false) yield break;
        _count = 0;
        yield return new WaitForSeconds(waitTime);
        Bullet(_specialBulletObj);
        //ここを修正する
        _keyCount++;
        yield break;
    }


    public override void Action()
    {
        Vector3 myRotate = transform.eulerAngles;
        transform.LookAt(Enemy.transform);
        rotation = transform.eulerAngles;
        transform.eulerAngles = myRotate;
    }

      public  void Bullet(GameObject bullet)
    {
        _enemy = Enemy.GetComponentInChildren<CharacterData>().gameObject;
        Vector3 scale = new Vector3(0,0,0);
        var obj = Instantiate(bullet);
        scale.y = 0.5f;
        obj.transform.position = transform.position;
        var value = _enemy.transform.position/*_enemyActiveModel.getBarrage.getCustomGameObject.transform.position*/ - transform.position +scale;
        obj.GetComponent<TestShot>()._vectorValue = value.normalized;
        obj.GetComponent<TestShot>()._parent = /*_enemyActiveModel.getBarrage.getCustomGameObject*/_enemy;
    }

}
