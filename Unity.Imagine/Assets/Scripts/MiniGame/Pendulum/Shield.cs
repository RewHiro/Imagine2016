using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    [SerializeField]
    string _ballName = "Ball";

    Pendulum pendulum = null;

    // Use this for initialization
    void Start() {
        pendulum = GetComponentInParent<Pendulum>();
    }

    // Update is called once per frame
    void Update() {
        if(pendulum == null)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == _ballName)
        {
            collision.gameObject.GetComponent<Ball>().ChangeTarget();
        }
    }

    //void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.gameObject.name == _ballName)
    //    {
    //        collider.gameObject.GetComponent<Ball>().ChangeTarget();
    //    }
    //}
}