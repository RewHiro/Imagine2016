using UnityEngine;
using System.Collections;

public class ParticleGeneration : MonoBehaviour {

    [SerializeField]
    ParticleSystem _particleSystem;

    TestShot _shot;

	void Start ()
    {
        _shot = GetComponent<TestShot>();
    }
	
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if (_shot._parent == null) { return; }
        if (_shot._parent.transform.name == collision.transform.name)
        {
            var particle = Instantiate(_particleSystem);
            particle.transform.position = gameObject.transform.position;
        }
    }
}
