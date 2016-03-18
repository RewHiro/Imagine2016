using UnityEngine;
using System.Collections;

public class Barrage : ActionManager
{

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
    GameObject _custom;

    [SerializeField]
    float _waitTime = 0.1f;

    float _time = 0;

    int _count = 0;

    RandomBullet _randomBullet;

    public int _getKeyCount { get { return _keyCount; } }

    void Start()
    {
        _time = _waitTime;
        _randomBullet = GetComponent<RandomBullet>();
        //_timeCount = GetComponent<TimeCount>();
    }

    void Update()
    {
        _keyCount += Barragebutton(keyCode);
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

    void WaitTime()
    {
        _time -= Time.deltaTime; 
    }

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
        //if (Input.GetKeyDown(keyCode) && _timeCount._getTime >= 1)
        //{
        //    Bullet();
        //}

    }

        void Bullet(GameObject bullet)
    {
        //Debug.Log(keyCode + " : ゲーム01テスト : " + Enemy.transform.name);s
        Vector3 enemyPosition = Enemy.transform.position;
        Vector3 scale = new Vector3(0,0,0);
        var obj = Instantiate(bullet);
        
        //obj.transform.position += transform.localScale;
        scale = transform.localScale;
        scale.z = 0;
        scale.x = 0;
        obj.transform.position = transform.position + scale;
        var value = _custom.transform.position - transform.position;
        //        var value = Enemy.transform.position - transform.position;
        obj.GetComponent<TestShot>()._vectorValue = value.normalized;
        obj.GetComponent<TestShot>()._parent = _custom;
    }

}
