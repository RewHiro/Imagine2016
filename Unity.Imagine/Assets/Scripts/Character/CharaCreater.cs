using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class CharaCreater : MonoBehaviour
{
    struct Paramter
    {
        public int attack { get; set; }
        public int defence { get; set; }
        public int speed { get; set; }
    }

    [SerializeField]
    Image _description = null;

    List<Sprite> _sprites = new List<Sprite>();

    Paramter[] _parameters = new Paramter[3];

    CharacterParameter _characterParamter;

    public CharacterParameter getCharacterParamter
    {
        get
        {
            return _characterParamter;
        }
    }

    #region typeset

    public void SetTypeAttack(int value)
    {
        _parameters[0].attack = value;
    }

    public void SetTypeDefence(int value)
    {
        _parameters[0].defence = value;
    }

    public void SetTypeSpeed(int value)
    {
        _parameters[0].speed = value;
    }

    #endregion

    #region costumeset

    public void SetCostumeAttack(int value)
    {
        _parameters[1].attack = value;
    }

    public void SetCostumeDefence(int value)
    {
        _parameters[1].defence = value;
    }

    public void SetCostumeSpeed(int value)
    {
        _parameters[1].speed = value;
    }

    #endregion

    #region decorationset

    public void SetDecorationAttack(int value)
    {
        _parameters[2].attack = value;
    }

    public void SetDecorationDefence(int value)
    {
        _parameters[2].defence = value;
    }

    public void SetDecorationSpeed(int value)
    {
        _parameters[2].speed = value;
    }

    #endregion

    public void SetType(int type)
    {
        if ((uint)type > (int)CharacterParameter.ModelType.NONE) throw new ArgumentException("type is error");
        _characterParamter.modelType = (CharacterParameter.ModelType)type;
        _description.sprite = _sprites[type];
    }

    public void SetCostumeType(int type)
    {
        if ((uint)type > (int)CharacterParameter.CostumeType.NONE) throw new ArgumentException("type is error");
        _characterParamter.costumeType = (CharacterParameter.CostumeType)type;
        _description.sprite = _sprites[type + 3];
    }

    public void SetDecorationType(int type)
    {
        if ((uint)type > (int)CharacterParameter.DecorationType.NONE) throw new ArgumentException("type is error");
        _characterParamter.decorationType = (CharacterParameter.DecorationType)type;
        _description.sprite = _sprites[type + 6];
    }

    public void Decide()
    {
        foreach (var parameter in _parameters)
        {
            _characterParamter.attack += parameter.attack;
            _characterParamter.defense += parameter.defence;
            _characterParamter.speed += parameter.speed;
        }
    }

    void Start()
    {
        _sprites.AddRange
            (
                    Resources.LoadAll<Sprite>("MakeOfCharacter/Texture/Description")
            );

        _description.sprite = _sprites[0];
    }
}
