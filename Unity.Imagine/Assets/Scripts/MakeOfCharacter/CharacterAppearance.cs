using UnityEngine;
using System.Collections;

public class CharacterAppearance : MonoBehaviour
{

    [SerializeField, Tooltip("出現ポジション")]
    private Vector3 _startPos = new Vector3(0.0f, 5.0f, 0.0f);

    [SerializeField, Tooltip("終了時ポジション")]
    private Vector3 _endPos = Vector3.zero;

    [SerializeField, Range(0.08f, 0.1f), Tooltip("１フレームに動く距離")]
    private float _movespeed = 0.08f;

    [SerializeField, Range(0.03f, 0.04f), Tooltip("回転する時間")]
    private float _rotateTime = 0.035f;

    void Start()
    {
        transform.localPosition = _startPos;
        transform.localRotation = Quaternion.Euler(180, 0, 0);
    }

    void Update()
    {
        Appearance();
    }

    void Appearance()
    {
        //マイフレーム呼び出す
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _endPos, _movespeed);
        //Quaternion.Slerp : 第一引数(Quaternion)から、第二引数(Quaternion)の方向に、第三引数(float)の時間をかけて回転する。
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), _rotateTime);

        //Quaternion.SlerpだとRotationがぴったしで止まってくれないので、座標がさだまったら向きを固定する
        if (transform.localPosition == _endPos)
        {
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }

    //ボタン押されたら登場座標にする
    public void CharacterChange()
    {
        transform.localPosition = _startPos;
        transform.localRotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);
    }

}
