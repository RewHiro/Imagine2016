using UnityEngine;
using System.Collections;

public class RandomBullet : MonoBehaviour {

    [SerializeField]
    CharacterParameterInfo _characterParameter;

    [SerializeField]
    int _randomMax = 11;
    
    int _speedStatus = 0;
	void Start ()
    {
        //_characterParameter = GetComponent<ModelParameterInfo>();
        _speedStatus = _characterParameter.getCharacterParameter.speed;
    }
	
	void Update ()
    {

    }

   public bool StatusRandomBullet()
    {
        int random = Random.Range(1, _randomMax);

        if(_speedStatus < random)
        {
            return true;
        }

        return false;
    }
}
