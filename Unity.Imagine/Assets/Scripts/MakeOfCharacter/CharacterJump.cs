using System;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{

    [SerializeField]
    private float _jumpForce = 100.0f;

    int LAYER_MASK = 0;

    bool _isJump = false;

    //private float _randomTime = 3.0f;

    //[SerializeField]
    //private float _minTime = 3.0f;
    //[SerializeField]
    //private float _maxTime = 5.0f;

    void Start()
    {
        string[] hitLayerNames = new string[1];
        hitLayerNames[0] = "TransparentFX";
        LAYER_MASK = LayerMask.GetMask(hitLayerNames);
    }

    void Update()
    {
        Gravity();
        JumpAction();
    }

    void Gravity()
    {
        if (!_isJump) return;

        var localPosition = transform.localPosition;

        if (localPosition.y > -0.01f) return;
        Debug.Log("HIT");
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.localPosition = new Vector3(localPosition.x, 0, localPosition.z);

        _isJump = false;
    }

    void JumpAction()
    {
        if (_isJump) return;

        if (!TouchController.IsTouchBegan()) return;

        RaycastHit raycastHit;
        if (!TouchController.IsRaycastHitWithLayer(out raycastHit, LAYER_MASK)) return;

        GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce);
        GetComponent<Rigidbody>().useGravity = true;
        _isJump = true;
    }
}
