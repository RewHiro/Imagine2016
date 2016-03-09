using UnityEngine;
using System.Collections;

public class Pendulum : ActionManager
{
    GameObject _shieldObj = null;
    public GameObject shieldObj
    {
        get
        {
            if(_shieldObj == null)
            {
                _shieldObj = Resources.Load("MiniGame/Pendulum/Shield") as GameObject;
            }
            return _shieldObj;
        }
    }

    // Use this for initialization
    void Start () {
        var shield = Instantiate(shieldObj);
        shield.transform.position = transform.position;
        shield.transform.rotation = transform.rotation;
        shield.transform.Translate(new Vector3(0.0f, 0.0f, 0.5f));
        shield.transform.parent = transform;
        shield.name = shieldObj.name;
        //_shield.transform.Translate(shield.transform.position);
        //transform.LookAt(Enemy.transform);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public override void Action()
    {
        transform.LookAt(Enemy.transform);
        if (Input.GetKeyDown(keyCode))
        {
            Debug.Log(keyCode + " : ペンデュラム : " + Enemy.transform.name);
        }
        if (Input.GetKey(keyCode))
        {
            transform.Rotate(5.0f, 0.0f, 0.0f);
        }
    }
}
