using UnityEngine;
using System.Collections;

public class ActiveModel : MonoBehaviour {

    Barrage[] _barrage;

   public Barrage getBarrage { get { return _barrage[0]; } }

    //ModelParameterInfo _modelParameterInfo;

    //public GameObject getCustomGameObject { get { return _modelParameterInfo.gameObject; } }


    void Awake()
    {
        _barrage = GetComponentsInChildren<Barrage>();
        Debug.Log(_barrage.Length);

    }

    void Start ()
    {
    }
	
	void Update ()
    {
	
	}
}
