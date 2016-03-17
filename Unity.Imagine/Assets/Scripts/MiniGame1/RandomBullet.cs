using UnityEngine;
using System.Collections;

public class RandomBullet : MonoBehaviour {
    CharacterParameter _characterParameter;

    int _speedStatus = 0;
	void Start ()
    {
        _characterParameter = GetComponent<CharacterParameter>();
        _speedStatus = _characterParameter.speed;
    }
	
	void Update ()
    {

    }

    void StatusRandomBullet()
    {
        int random = Random.Range(1, 11);

        if(_speedStatus < random)
        {

        }
        //Debug.Log(Random.Range(1, 11));
    }
}
