using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    [SerializeField]
    GameObject[] _player = null;

    float power { get; set; }

    [SerializeField]
    float _maxPower = 50.0f;

    [SerializeField]
    float _initPower = 5.0f;

    [SerializeField]
    float _addPowerValue = 5.0f;

    Rigidbody _rigidbody; 

    enum Target{
        Player1,
        Player2,
    }
    Target target = Target.Player1;

    // Use this for initialization
    void Start () {
        init();
    }

    void init()
    {
        //Vector3 vector = _player[0].transform.position - transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        power = _initPower;
    }
	
	// Update is called once per frame
	void Update () {
        ThrowBall();
    }

    void ThrowBall()
    {
        //Debug.Log((int)target);
        Vector3 vectorPower = _player[(int)target].transform.position - transform.position;
        _rigidbody.velocity = vectorPower.normalized * power;
        //_rigidbody.AddForce(vectorPower.normalized);
    }

    public void ChangeTarget()
    {
        target = target == Target.Player1 ? Target.Player2 : Target.Player1;
        AddPower();
    }

    void AddPower()
    {
        power += power < _maxPower ? _addPowerValue : 0;
    }

    public void SetPlayers(GameObject[] players)
    {
        _player = players;
    }
}
