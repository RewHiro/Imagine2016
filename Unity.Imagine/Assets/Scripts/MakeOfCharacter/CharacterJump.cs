using System;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{

    [SerializeField]
    private float _jumpForce = 100.0f;

    //private float _randomTime = 3.0f;

    //[SerializeField]
    //private float _minTime = 3.0f;
    //[SerializeField]
    //private float _maxTime = 5.0f;

    void Start()
    {

    }

    void Update()
    {
        //_randomTime -= Time.deltaTime;
        //if(_randomTime <= 0.0f)
        //{
        //    _randomTime = UnityEngine.Random.Range(_minTime, _maxTime);
        //    JumpAction();
        //}

        var localPosition = transform.localPosition;

        if (localPosition.y > 0.0f) return;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.localPosition = new Vector3(localPosition.x, 0, localPosition.z);

    }

    public void JumpAction()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce);
        GetComponent<Rigidbody>().useGravity = true;
    }
}
