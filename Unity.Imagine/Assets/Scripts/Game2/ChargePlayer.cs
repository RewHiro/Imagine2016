using UnityEngine;
using System.Collections;

public class ChargePlayer : ActionManager
{
    [SerializeField]
    private Gage _gage;

    bool _pressOnce = false;

    void Start()
    {

    }

    void Update()
    {
        IsKeyDownMoveGage();
    }

    void IsKeyDownMoveGage()
    {
        if (_pressOnce) return;

        if (Input.GetKey(keyCode))
        {
            _gage.MoveSelectGage();
        }
        else
            if (Input.GetKeyUp(keyCode))
        {
            _pressOnce = true;
        }
    }
}
