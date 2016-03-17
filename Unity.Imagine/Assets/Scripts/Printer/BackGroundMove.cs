using UnityEngine;
using System.Collections;

public class BackGroundMove : MonoBehaviour {

    [SerializeField, Tooltip("X方向に進むスピード, もう一方と同じスピードにすること")]
    private float _moveSpeed = 0.1f;
    [SerializeField, Tooltip("スタートの背景のX座標")]
    private int _startPosX;

    /// <summary>
    /// キャンバスのサイズ
    /// </summary>
    private const int _returnPosX = 1920;
     
	void Start ()
    {
        transform.localPosition = new Vector3(_startPosX, 0.0f, 0.0f);
	}
	
	void Update ()
    {
        transform.Translate(_moveSpeed, 0.0f, 0.0f);
        if(transform.localPosition.x > _returnPosX)
        {
            transform.localPosition = new Vector3(-_returnPosX, 0.0f, 0.0f);
        }
	}
}
