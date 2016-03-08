using UnityEngine;
using System.Collections;

public class GameTest1 : ActionManager
{
    [SerializeField]
    GameObject _bulletObj = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Action()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Debug.Log(keyCode + " : ゲーム01テスト : " + Enemy.transform.name);

            var obj = Instantiate(_bulletObj);
            obj.transform.position = transform.position;
            var value = Enemy.transform.position - transform.position;
            obj.GetComponent<TestShot>()._vectorValue = value.normalized;
            obj.GetComponent<TestShot>()._parent = gameObject;
        }
       //if (Input.GetKey(keyCode)) {
       //     transform.Rotate(5.0f, 0.0f, 0.0f);
       // }
    }
}
