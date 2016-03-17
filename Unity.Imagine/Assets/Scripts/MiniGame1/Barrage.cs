using UnityEngine;
using System.Collections;

public class Barrage : ActionManager
{

    [SerializeField]
    GameObject _bulletObj = null;

    //[SerializeField]
    private int _keyCount = 0;

    [SerializeField]
    TimeCount _timeCount = null;

    [SerializeField]
    int _probability = 0;

    [SerializeField]
    GameObject _custom;



    int _count = 0;

    RandomBullet _randomBullet;

    public int _getKeyCount { get { return _keyCount; } }

    void Start()
    {
        _randomBullet = GetComponent<RandomBullet>();
        //_timeCount = GetComponent<TimeCount>();
    }

    void Update()
    {
        _keyCount += Barragebutton(keyCode);
    }

    int Barragebutton(KeyCode key)
    {
        if (_timeCount._getTime <= 0) return 0;
        if (Input.GetKeyDown(key))
        {
            _count++;
           if (_probability > _count) return 1;
            if(_randomBullet.StatusRandomBullet() == false) return 1;
            Debug.Log("homo");
            _count = 0;
            Bullet();
            return 2;
        }

        return 0;
    }

    public override void Action()
    {
        if (Input.GetKeyDown(keyCode) && _timeCount._getTime > 1)
        {
            Bullet();
        }
    }

    void Bullet()
    {
        //Debug.Log(keyCode + " : ゲーム01テスト : " + Enemy.transform.name);s
        Vector3 enemyPosition = Enemy.transform.position;
        Vector3 scale = new Vector3(0,0,0);
        var obj = Instantiate(_bulletObj);
        
        //obj.transform.position += transform.localScale;
        scale = transform.localScale;
        //Debug.Log(transform.localScale);
        scale.z = 0;
        scale.x = 0;
        obj.transform.position = transform.position + scale;
        var value = _custom.transform.position - transform.position;
        //        var value = Enemy.transform.position - transform.position;
        obj.GetComponent<TestShot>()._vectorValue = value.normalized;
        obj.GetComponent<TestShot>()._parent = gameObject;
    }

}
