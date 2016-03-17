using UnityEngine;
using System.Collections;

public class BackGroundMove : MonoBehaviour {

    private enum MoveDirection
    {
        X,
        Y
    }

    [SerializeField]
    private MoveDirection _direction;

    [SerializeField, Tooltip("X方向に進むスピード, もう一方と同じスピードにすること")]
    private float _moveSpeed = 0.1f;
    [SerializeField, Tooltip("スタートの背景の座標, X = 1920, Y = 1080")]
    private int _startPos;

    private Vector2 ReturnPos = new Vector2(1920, 1080);

    /// <summary>
    /// キャンバスのサイズ
    /// </summary>
	void Start ()
    {
        if(_direction == MoveDirection.X) transform.localPosition = new Vector3(_startPos, 0.0f, 0.0f);
        else if(_direction == MoveDirection.Y) transform.localPosition = new Vector3(0.0f, _startPos, 0.0f);
    }

    void Update ()
    {
        if(_direction == MoveDirection.X)
        {
            transform.Translate(_moveSpeed, 0.0f, 0.0f);
            if (transform.localPosition.x > ReturnPos.x)
            {
                transform.localPosition = new Vector3(-ReturnPos.x, 0.0f, 0.0f);
            }
        }
        else if(_direction == MoveDirection.Y)
        {
            transform.Translate(0.0f, _moveSpeed, 0.0f);
            if(transform.localPosition.y > ReturnPos.y)
            {
                transform.localPosition = new Vector3(0.0f, -ReturnPos.y, 0.0f);
            }
        }
    }
}
