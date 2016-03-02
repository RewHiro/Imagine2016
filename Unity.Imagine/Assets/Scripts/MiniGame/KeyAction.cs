using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyAction : MonoBehaviour {

    [SerializeField]
    private KeyCode[] _key = { KeyCode.A, KeyCode.D };

    private ActionManager[] _actionMgr;

    // Use this for initialization
    void Start () {
        _actionMgr = GetComponentsInChildren<ActionManager>();

        if(_actionMgr.Length < 2) { return; }

        float max_x = -10000;
        int max_index = -1;
        float min_x = 10000;
        int min_index = -1;
        for (int i = 0; i < _actionMgr.Length; ++i)
        {
            if(_actionMgr[i].transform.position.x > max_x)
            {
                max_x = _actionMgr[i].transform.position.x;
                max_index = i;
            }
            if (_actionMgr[i].transform.position.x < min_x)
            {
                min_x = _actionMgr[i].transform.position.x;
                min_index = i;
            }
        }

        _actionMgr = new ActionManager[] { _actionMgr[min_index], _actionMgr[max_index] };
        for (int i = 0; i < _actionMgr.Length; ++i)
        {
            _actionMgr[i].keyCode = _key[i];           
        }
        _actionMgr[0].GetComponent<MeshRenderer>().material.color = Color.red;
        _actionMgr[1].GetComponent<MeshRenderer>().material.color = Color.blue;
    }
	
	// Update is called once per frame
	void Update () {
        KeysAction();
    }

    void KeysAction()
    {
        if (_actionMgr.Length < 2) { return; }
        foreach (var action in _actionMgr)
        {
            action.Action();
        }
    }
}
