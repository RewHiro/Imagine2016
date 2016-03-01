using UnityEngine;
using System.Collections;

public class MouseUtility : MonoBehaviour
{

    public Vector3 getDeltaPos
    {
        get; private set;
    }

    Vector3 _oldPos;

    void Start()
    {
        _oldPos = Input.mousePosition;
    }

    void Update()
    {
        var currentPos = Input.mousePosition;
        getDeltaPos = currentPos - _oldPos;
        _oldPos = currentPos;
    }
}
