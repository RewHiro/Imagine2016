using UnityEngine;
using System.Collections;

public class Barrage : ActionManager
{

    [SerializeField]
    GameObject _bulletObj = null;

    [SerializeField]
    private int _keyCount = 0;

    [SerializeField]
    TimeCount _timeCount = null;

    public int _getKeyCount { get { return _keyCount; } }

    void Start()
    {
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
            return 1;
        }

        return 0;
    }

    public override void Action()
    {
        if (Input.GetKeyDown(keyCode) && _timeCount._getTime > 1)
        {
            Debug.Log(keyCode + " : ゲーム01テスト : " + Enemy.transform.name);
            Vector3 homo = Enemy.transform.position;
            var obj = Instantiate(_bulletObj);
            obj.transform.position = transform.position + transform.localScale/2;
           homo.y -= transform.localScale.y;
            var value = homo - transform.position;
            obj.GetComponent<TestShot>()._vectorValue = value.normalized;
            obj.GetComponent<TestShot>()._parent = gameObject;
        }
    }

}
