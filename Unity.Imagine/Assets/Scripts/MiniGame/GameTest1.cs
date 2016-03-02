using UnityEngine;
using System.Collections;

public class GameTest1 : ActionManager
{

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
            Debug.Log(keyCode + " : ゲーム01テスト");
        }
        if (Input.GetKey(keyCode)) {
            transform.Rotate(5.0f, 0.0f, 0.0f);
        }
    }
}
