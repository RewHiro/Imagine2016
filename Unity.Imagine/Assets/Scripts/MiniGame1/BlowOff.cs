using UnityEngine;
using System.Collections.Generic;

public class BlowOff : MonoBehaviour {


    ScoreCompare _scoreCompare;

    KeyAction _actionManager;

    void Start () {
        _scoreCompare = FindObjectOfType<ScoreCompare>();
        _actionManager = FindObjectOfType<KeyAction>();
    }
	
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {


        if (_scoreCompare.getDisplayScore ==true)
        {
            List<GameObject> _playerList = new List<GameObject>();
            _playerList = _actionManager.GetPlayers();

            if (gameObject.transform.parent.gameObject == _playerList[0])
            {
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-50, 50, 0), ForceMode.VelocityChange);
            }
            else
            if (gameObject.transform.parent.gameObject == _playerList[1])
            {
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(50, 50, 0), ForceMode.VelocityChange);
            }

        }
    }
}
