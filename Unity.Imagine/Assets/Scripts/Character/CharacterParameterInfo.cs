using UnityEngine;
using System;

public class CharacterParameterInfo : MonoBehaviour
{
    CharacterParameter _characterParameter;

    public CharacterParameter getCharacterParameter
    {
        get
        {
            return _characterParameter;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Decide()
    {
        var changeCharacterPattern = FindObjectOfType<ChangeCharacterPattern>();
        if (changeCharacterPattern == null) throw new NullReferenceException("ChangeCharacterPattern is nothing");
        _characterParameter = changeCharacterPattern.getCharacterParamter;
    }
}
