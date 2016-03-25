using UnityEngine;
using System.Collections;

public class FireWorksCreate : MonoBehaviour {

    [SerializeField]
    float _coolDownTime = 1;

    float _time;

    [SerializeField]
    GameObject _particle;

	void Start ()
    {
        _time = _coolDownTime;
	}
	
	void Update ()
    {
        if (CresteCoolDown())
        {
            Create();
            _time = _coolDownTime;
        }
	}

    bool CresteCoolDown()
    {
        _time -= Time.deltaTime;

        
        if (_time <= 0) return true;
        return false;

    }

    void Create()
    {
        GameObject particleObj = Instantiate(_particle);
        particleObj.transform.SetParent(gameObject.transform, false);
    }

}
