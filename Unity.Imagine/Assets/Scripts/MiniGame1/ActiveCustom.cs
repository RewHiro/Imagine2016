using UnityEngine;
using System.Collections;

public class ActiveCustom : MonoBehaviour {

    ModelParameterInfo[] _modelParameterInfo;

    GameObject _gameObject {get { return _modelParameterInfo[0].gameObject; } }

    void Awake()
    {
        _modelParameterInfo = gameObject.GetComponentsInChildren<ModelParameterInfo>();
        Debug.Log(_modelParameterInfo.Length);
    }

	void Start ()
    {
	
	}
	
	
	void Update ()
    {
	
	}
}
