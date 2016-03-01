using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// キャラクターを回転する機能
/// </summary>
public class CharacterViewController : MonoBehaviour
{

    [SerializeField, TooltipAttribute("無視したくないLayerNameを設定")]
    string[] _layerNames = null;

    [SerializeField, TooltipAttribute("カメラで写したいターゲット")]
    Transform _target = null;

    [SerializeField, Range(1.0f, 100.0f), TooltipAttribute("カメラとターゲットの距離")]
    float DISTANCE = 5.0f;

    [SerializeField, Range(0.0f, 10.0f), TooltipAttribute("加速度する時間(秒)")]
    float ACCELERATION_TIME = 5.0f;

    Vector3 POINT = Vector3.zero;

    int LAYER_MASK = 0;

    Vector2 _deltaPosition = Vector2.zero;
    MouseUtility _mouseUtility = null;
    float _count = 0.0f;

    void Awake()
    {
        _count = ACCELERATION_TIME;
    }

    void Start()
    {
        if (_layerNames.Length == 0) throw new IndexOutOfRangeException("index is empty");
        if (_target == null) throw new NullReferenceException("target is empty");

        _mouseUtility = FindObjectOfType<MouseUtility>();
        if (_mouseUtility == null) throw new NullReferenceException("MouseUtility is nothing");

        LAYER_MASK = LayerMask.GetMask(_layerNames);

        POINT = _target.position;

        gameObject.transform.position =
            new Vector3(POINT.x, POINT.y, -DISTANCE);

        StartCoroutine(Control());
    }

    IEnumerator Control()
    {
        while (true)
        {
            StartCoroutine(Rotate());
            StartCoroutine(AccelerationRotate());
            yield return null;
        }
    }

    IEnumerator Rotate()
    {
        if (!TouchController.IsTouchMoved()) yield break;

        _count = ACCELERATION_TIME;
        RaycastHit raycastHit;
        if (!TouchController.IsRaycastHitWithLayer(out raycastHit, LAYER_MASK)) yield break;

        _deltaPosition = TouchController.IsSmartDevice ?
            Input.touches[0].deltaPosition :
            new Vector2(_mouseUtility.getDeltaPos.x, _mouseUtility.getDeltaPos.y);

        gameObject.transform.RotateAround(POINT, gameObject.transform.up, _deltaPosition.x);
        gameObject.transform.RotateAround(POINT, -gameObject.transform.right, _deltaPosition.y);
    }

    IEnumerator AccelerationRotate()
    {
        if (_count <= 0.0f) yield break;

        _count += -Time.deltaTime;
        var acceleration = _deltaPosition * _count / ACCELERATION_TIME;
        gameObject.transform.RotateAround(POINT, gameObject.transform.up, acceleration.x);
        gameObject.transform.RotateAround(POINT, -gameObject.transform.right, acceleration.y);

    }
}
