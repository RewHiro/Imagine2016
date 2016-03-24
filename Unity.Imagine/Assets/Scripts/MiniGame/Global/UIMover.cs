using UnityEngine;
using System.Collections;

public class UIMover : MonoBehaviour {

    [SerializeField]
    GamePlayManager _playMgr;

    [SerializeField]
    Vector3 _targetPos;

    [SerializeField]
    float _speed = 10.0f;

    RectTransform rect { get; set; }

	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_playMgr.gamePlayFlag) { return; }
        rect.anchoredPosition3D -= (rect.anchoredPosition3D - _targetPos) / _speed;
	}
}
