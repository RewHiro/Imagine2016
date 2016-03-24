using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour {

    [SerializeField]
    GamePlayManager _playMgr;

    [SerializeField]
    Vector3 _targetPos;

    [SerializeField]
    float _speed = 10.0f;

    Vector3 _initPos;

    // Use this for initialization
    void Start()
    {
        _initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playMgr.gamePlayFlag) {
            transform.position = _initPos;
            return;
        }
        transform.position -= (transform.position - _targetPos) / _speed;
    }
}
