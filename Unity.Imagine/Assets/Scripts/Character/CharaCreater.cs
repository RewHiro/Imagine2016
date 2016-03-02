using UnityEngine;

public class CharaCreater : MonoBehaviour
{
    CharacterParameter _characterParamter;

    public CharacterParameter getCharacterParamter
    {
        get
        {
            return _characterParamter;
        }
    }

    public void SetAttack(uint value)
    {
        _characterParamter.attack = value;
    }

    public void SetDefence(uint value)
    {
        _characterParamter.defense = value;
    }

    public void SetSpeed(uint value)
    {
        _characterParamter.speed = value;
    }

    //public void SetType(uint value)
    //{
    //    _characterParamter.type = value;
    //}

    //public void SetCostumeID(uint value)
    //{
    //    _characterParamter.costumeID = value;
    //}

    public void Decide()
    {
        //TODO:外部出力でパラメータ保存
    }
}
