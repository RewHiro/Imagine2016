using UnityEngine;
using System.Collections;

public class ChargePlayer : ActionManager
{
    [SerializeField]
    private Gage[] _gage;

    bool _pressOnce = false;

    public bool _getPressOnce { get { return _pressOnce; } }

    [SerializeField]
    private EnergyGage[] _energyGage;

    void Start(){}

    void Update()
    {
        IsKeyDownMoveGage();
        EnergyGageMove();
    }

    void IsKeyDownMoveGage()
    {
        if (_pressOnce) return;

        if (Input.GetKey(keyCode))
        {
            _gage[0].MoveSelectGage();
        }
        else
            if (Input.GetKeyUp(keyCode))
        {
            _gage[0].RangeSelectNow();
            _pressOnce = true;
        }
    }

    void EnergyGageMove()
    {
        if (!_pressOnce) return;

        //Debug.Log(_energyGage[0]._getIsPowerGage);

        if (_energyGage[0].PowerGage() == true)
        {
            Init();
        }

    }

    void Init()
    {
        //if (!_pressOnce) return;
        //if (!_isEnergyGage) return;
        int finishPowerGageCount = 0;
        foreach (var energyGage in _energyGage)
        {
            if (energyGage._getIsPowerGage == true)
            {
                finishPowerGageCount++;
            }
        }

        if (finishPowerGageCount == _energyGage.Length)
        {
            _gage[0].InitGage();
            _pressOnce = false;
        }
    }
}
