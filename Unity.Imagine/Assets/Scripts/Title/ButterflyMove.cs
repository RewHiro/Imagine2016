using UnityEngine;
using System.Collections;

public class ButterflyMove : MonoBehaviour
{
    [SerializeField]
    GameObject _image;

    private float _sinWaveCount = 0.0f;

    private float _moveSpeed = -60.0f;

    void Start()
    {
    }

    void Update()
    {
        UpdateOfMove();
    }

    private void UpdateOfMove()
    {
        _sinWaveCount += Time.deltaTime * 5;

        _image.transform.localPosition =
            new Vector3(_image.transform.localPosition.x + _moveSpeed * Time.deltaTime,
                        _image.transform.localPosition.y + UnityEngine.Mathf.Sin(_sinWaveCount),
                        _image.transform.localPosition.z);

        if (_image.transform.localPosition.x < -160)
        {
            _moveSpeed *= -1;
            _image.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (_image.transform.localPosition.x > 160)
        {
            _moveSpeed *= -1;
            _image.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
