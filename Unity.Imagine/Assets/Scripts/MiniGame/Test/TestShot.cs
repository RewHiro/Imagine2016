using UnityEngine;
using System.Collections;

public class TestShot : MonoBehaviour {

    public Vector3 _vectorValue { get; set; }
    public GameObject _parent { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += _vectorValue;
	}

    void OnTriggerEnter(Collider collision)
    {
        if(_parent == null) { return; }

        if (_parent.transform.name == collision.transform.name)
        {
            Destroy(gameObject);
        }
    }
}
