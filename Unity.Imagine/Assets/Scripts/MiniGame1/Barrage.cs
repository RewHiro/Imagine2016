using UnityEngine;
using System.Collections;

public class Barrage : ActionManager
{
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

    [SerializeField]
    ActiveModel _enemyActiveModel;

    [SerializeField]
    float _waitTime = 0.1f;

    float _time = 0;

    int _count = 0;

    RandomBullet _randomBullet;

    public int _getKeyCount { get { return _keyCount; } }

    CharacterParameterInfo _characterParameterInfo;

    public GameObject getCustomGameObject { get { return _characterParameterInfo.gameObject; } }


    void Awake()
    {
    //    _modelParameterInfo = GetComponentInChildren<ModelParameterInfo>();
     //   Debug.Log(_modelParameterInfo.name);

    }

    void Start()
    {
        
        if (_startCount == null)
        {
            _startCount = GameObject.Find("StartCount").GetComponent<StartCount>();
        }

        if (_timeCount == null)
        {
            _timeCount = GameObject.Find("Time").GetComponent<TimeCount>();
        }

        _time = _waitTime;
        _randomBullet = GetComponent<RandomBullet>();
        //_timeCount = GetComponent<TimeCount>();

        _characterParameterInfo = GetComponentInChildren<CharacterParameterInfo>();
        //Debug.Log(_characterParameterInfo.gameObject.name);

    }

    void Update()
    {
        if (_startCount.getCountFinish)
        {
            _keyCount += Barragebutton(keyCode);
        }
    }

    //int Barragebutton(KeyCode key)
    //{
    //    if (_timeCount._getTime <= 0) return 0;
    //    if (Input.GetKeyDown(key))
    //    {
    //        _count++;
    //        if (_probability > _count) return 1;
    //        if (_randomBullet.StatusRandomBullet() == false) return 1;
    //        _count = 0;

    //        Bullet();
    //        return 2;
    //    }

    //    return 0;
    //}


    int Barragebutton(KeyCode key)
    {
        if (_timeCount._getTime <= 0) return 0;
        if (Input.GetKeyDown(key))
        {
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
        transform.LookAt(Enemy.transform);
        rotation = transform.eulerAngles;
    }

        void Bullet(GameObject bullet)
    {
        //Debug.Log(keyCode + " : ゲーム01テスト : " + Enemy.transform.name);s
        Vector3 enemyPosition = Enemy.transform.position;
        Vector3 scale = new Vector3(0,0,0);
        var obj = Instantiate(bullet);
        
        //obj.transform.position += transform.localScale;
        //scale = transform.localScale;
        //scale.z = 0;
        //scale.x = 0;
        obj.transform.position = transform.position;
        var value = _enemyActiveModel.getBarrage.getCustomGameObject.transform.position - transform.position;
        //        var value = Enemy.transform.position - transform.position;
        obj.GetComponent<TestShot>()._vectorValue = value.normalized;
        obj.GetComponent<TestShot>()._parent = _enemyActiveModel.getBarrage.getCustomGameObject;
    }

}
