using UnityEngine;
using System.Collections;

public class ParticleGeneration : MonoBehaviour {

    [SerializeField]
    ParticleSystem _particleSystem;

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision collision)
    {
       //if()
        {
            var particle = Instantiate(_particleSystem);
            particle.transform.position = gameObject.transform.position;
        }
    }
}
