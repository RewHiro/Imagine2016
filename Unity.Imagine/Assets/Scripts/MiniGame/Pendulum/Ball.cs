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

    bool _stopFlag = false;

    enum Target{
        Player1,
        Player2,
    }
    Target target = Target.Player1;

    // Use this for initialization
    void Start () {
        init();

        Vector3 offset = new Vector3(0.0f, 1.0f, 0.0f);
        transform.position += (offset);
    }

    void init()
    {
        //Vector3 vector = _player[0].transform.position - transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        power = _initPower;
    }
	
	// Update is called once per frame
	void Update () {
        if (_stopFlag) { return; }
        ThrowBall();
    }

    void ThrowBall()
    {
        //Debug.Log((int)target);
        Vector3 vectorPower = _player[(int)target].transform.position - transform.position;

        float ballLength = Vector3.Distance(_player[(int)target].transform.position, transform.position);
        float playerLength = Vector3.Distance(_player[0].transform.position, _player[1].transform.position);
        float nomarize = ballLength / playerLength;
        Vector3 sin = new Vector3(0.0f, -Mathf.Cos(nomarize * Mathf.PI), 0.0f);

        float offsetY = 7.0f;
        Vector3 offset = new Vector3(0.0f, offsetY / 2.5f, 0.0f);
        _rigidbody.velocity = vectorPower.normalized * power + sin * offsetY + offset;
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

    public void StopBall()
    {
        _stopFlag = true;
        _rigidbody.velocity = Vector3.zero;
    }
}
