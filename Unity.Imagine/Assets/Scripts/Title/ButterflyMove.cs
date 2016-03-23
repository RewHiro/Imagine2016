using UnityEngine;
using System.Collections;

public class ButterflyMove : MonoBehaviour
{
    [SerializeField, Tooltip("サイズ変更用")]
    private float _scale = 1.0f;

    private float _sinWaveCount = 0.0f;

    [SerializeField, Tooltip("スピード変更用")]
    private float _moveSpeed = 60.0f;

    [SerializeField, Tooltip("縦の動きの幅")]
    private int _sinMove = 5;

    void Start()
    {
        transform.localScale = Vector3.one * _scale;
    }

    void Update()
    {
        UpdateOfMove();
    }

    private void UpdateOfMove()
    {
        _sinWaveCount += Time.deltaTime * _sinMove;

        transform.localPosition =
            new Vector3(transform.localPosition.x + _moveSpeed * Time.deltaTime,
                        transform.localPosition.y + UnityEngine.Mathf.Sin(_sinWaveCount),
                        transform.localPosition.z);

        if (transform.localPosition.x < -160)
        {
            _moveSpeed *= -1;
            transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (transform.localPosition.x > 160)
        {
            _moveSpeed *= -1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            //transform.localPosition =
            //new Vector3(transform.localPosition.x,
            //            transform.localPosition.y,
            //            transform.localPosition.z);
        }
    }
}
