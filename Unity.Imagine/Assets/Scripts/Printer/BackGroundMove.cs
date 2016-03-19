using UnityEngine;
using System.Collections;

public class BackGroundMove : MonoBehaviour {

    private enum MoveDirection
    {
        PLUSX,
        PLUSY,
        PLUSXY,
        MINUSX,
        MINUSY,
        MINUSXY,
    }

    [SerializeField]
    private MoveDirection _direction;

    [SerializeField, Tooltip("X方向に進むスピード, もう一方と同じスピードにすること")]
    private float _moveSpeed = 0.1f;
    [SerializeField, Tooltip("最初の座標 １.-420, 2.660 3.-1500")]
    private Vector3 _startPos;

    private const int _returnX = 1550;

    private const int _resetX = -1680;
    
    /// <summary>
    /// キャンバスのサイズ
    /// </summary>
	void Start ()
    {
        transform.localPosition = _startPos;
    }

    void Update ()
    {
        if(_direction == MoveDirection.PLUSX)
        {
            transform.Translate(_moveSpeed, 0.0f, 0.0f);
            if (transform.localPosition.x > _returnX)
            {
                transform.localPosition = new Vector3(_resetX, 0.0f, 0.0f);
            }
        }
        else if(_direction == MoveDirection.MINUSX)
        {
            transform.Translate(-_moveSpeed, 0.0f, 0.0f);
            if(transform.localPosition.x < -_returnX)
            {
                transform.localPosition = new Vector3(-_resetX, 0.0f, 0.0f);
            }
        }
        //else if(_direction == MoveDirection.PLUSY)
        //{
        //    transform.Translate(0.0f, _moveSpeed, 0.0f);
        //    if(transform.localPosition.y > _returnX.y)
        //    {
        //        transform.localPosition = new Vector3(0.0f, -_returnX.y, 0.0f);
        //    }
        //}
        
        
    }
}
