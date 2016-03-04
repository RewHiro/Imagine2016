using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class CharaCreater : MonoBehaviour
{

    [SerializeField]
    Image _description = null;

    List<Sprite> _sprites = new List<Sprite>();

    CharacterParameter _characterParamter = new CharacterParameter();
    ParameterBar _parameterBar = null;

    public CharacterParameter getCharacterParamter
    {
        get
        {
            return _characterParamter;
        }
    }

    public void SetType(int type)
    {
        if ((uint)type > (int)CharacterParameter.ModelType.NONE) throw new ArgumentException("type is error");
        _characterParamter.modelType = (CharacterParameter.ModelType)type;
        _description.sprite = _sprites[type];
        Decide();
    }

    public void SetCostumeType(int type)
    {
        if ((uint)type > (int)CharacterParameter.CostumeType.NONE) throw new ArgumentException("type is error");
        _characterParamter.costumeType = (CharacterParameter.CostumeType)type;
        _description.sprite = _sprites[type + 3];
        Decide();
    }

    public void SetDecorationType(int type)
    {
        if ((uint)type > (int)CharacterParameter.DecorationType.NONE) throw new ArgumentException("type is error");
        _characterParamter.decorationType = (CharacterParameter.DecorationType)type;
        _description.sprite = _sprites[type + 6];
        Decide();
    }

    public void Decide()
    {
        _characterParamter.attack = 0;
        _characterParamter.defense = 0;
        _characterParamter.speed = 0;
        foreach (var parameter in FindObjectsOfType<ModelParameterInfo>())
        {
            _characterParamter.attack += parameter.getModelParameter.attack;
            _characterParamter.defense += parameter.getModelParameter.defence;
            _characterParamter.speed += parameter.getModelParameter.speed;
        }

        Debug.Log(_parameterBar);
        _parameterBar.ChangeParameterGauge();

        Debug.Log(_characterParamter.attack);
        Debug.Log(_characterParamter.defense);
        Debug.Log(_characterParamter.speed);
        Debug.Log(_characterParamter.modelType);
        Debug.Log(_characterParamter.costumeType);
        Debug.Log(_characterParamter.decorationType);
    }

    void Start()
    {
        _sprites.AddRange
            (
                    Resources.LoadAll<Sprite>("MakeOfCharacter/Texture/Description")
            );

        _description.sprite = _sprites[0];

        _parameterBar = FindObjectOfType<ParameterBar>();

        Decide();
        _characterParamter.modelType = CharacterParameter.ModelType.HUMAN;
        _characterParamter.costumeType = CharacterParameter.CostumeType.A;
        _characterParamter.decorationType = CharacterParameter.DecorationType.A;
    }
}
