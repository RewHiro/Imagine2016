using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterParameter _characterParamter;

    public CharacterParameter getCharacterParamter
    {
        get
        {
            return _characterParamter;
        }
    }

    void Start()
    {
        //TODO：外部からパラメータを読み込む
    }
}
