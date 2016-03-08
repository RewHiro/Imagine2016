using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/*
2 / 29
14 : 40 野本　変更

    前回のをかえて、
    Listに処理をいれ関数を一々作るのを辞めました

    各ボタンごとに番号をしていてやっています
    Test
*/

public class ChangeCharacterPattern : MonoBehaviour
{
    [SerializeField]
    GameObject[] _panels = null;

    //Typeのモデル
    [SerializeField]
    GameObject[] _typePrefabs = null;

    //Costumeのモデル
    [SerializeField]
    GameObject[] _costumePrefabs = null;

    //Decorationのモデル
    [SerializeField]
    GameObject[] _decorationPrefabs = null;

    [SerializeField]
    GameObject _characterPlace = null;

    [SerializeField]
    Image _description = null;

    List<Sprite> _sprites = new List<Sprite>();

    GameObject _character = null;

    CharacterParameter _characterParamter = new CharacterParameter();
    ParameterBar _parameterBar = null;
    CharacterParameterInfo _characterParameterInfo = null;

    public CharacterParameter getCharacterParamter
    {
        get
        {
            return _characterParamter;
        }
    }

    public void Decide()
    {
        StartCoroutine(DecideCorutine());
        StartCoroutine(Transition());
    }

    //TypeButtonを押したら
    public void PushButtonOfType()
    {
        _panels[0].SetActive(true);
        _panels[1].SetActive(false);
        _panels[2].SetActive(false);
    }

    //CostumeButtonを押したら
    public void PushButtonOfCostume()
    {
        _panels[0].SetActive(false);
        _panels[1].SetActive(true);
        _panels[2].SetActive(false);
    }

    //DecorationButtonを押したら
    public void PushButtonOfDecoration()
    {
        _panels[0].SetActive(false);
        _panels[1].SetActive(false);
        _panels[2].SetActive(true);
    }

    public void PushOfDecideButton()
    {
        //決定ボタンを押したら
    }

    //
    public void PushOfBackTitle()
    {
        //右上のButtonを押したら
    }

    public void SetType(int index)
    {
        if ((uint)index > (int)CharacterParameter.ModelType.NONE) throw new ArgumentException("type is error");

        _character = CreateModel((uint)index, _typePrefabs, _character.transform.parent);
        SetCostume((int)getCharacterParamter.costumeType);

        _characterParamter.modelType = (CharacterParameter.ModelType)index;
        _description.sprite = _sprites[index];
        StartCoroutine(DecideCorutine());
    }

    public void SetCostume(int index)
    {
        if ((uint)index > (int)CharacterParameter.CostumeType.NONE) throw new ArgumentException("costume is error");

        var place = _character.transform.GetChild(0);
        CreateModel((uint)index, _costumePrefabs, place);

        _characterParamter.costumeType = (CharacterParameter.CostumeType)index;
        _description.sprite = _sprites[index + 3];
        StartCoroutine(DecideCorutine());
    }

    public void SetDecoration(int index)
    {
        if ((uint)index > (int)CharacterParameter.DecorationType.NONE) throw new ArgumentException("type is error");

        //CreateModel((uint)index, _decorationPrefabs, "Decoration");

        _characterParamter.decorationType = (CharacterParameter.DecorationType)index;
        _description.sprite = _sprites[index + 6];
        StartCoroutine(DecideCorutine());
    }

    GameObject CreateModel(uint index, GameObject[] prefabs, Transform parent)
    {
        if (index > prefabs.Length) throw new IndexOutOfRangeException("out of range");

        var child = parent.GetChild(0);
        if (child == null) throw new NullReferenceException(parent.name + " is nothing child");
        Destroy(child.gameObject);

        var model = Instantiate(prefabs[index]);
        model.name = prefabs[index].name;
        model.transform.SetParent(parent);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;

        return model;
    }

    void Start()
    {
        _sprites.AddRange
        (
            Resources.LoadAll<Sprite>("MakeOfCharacter/Texture/Description")
        );

        _character = _characterPlace.transform.GetChild(0).gameObject;

        _description.sprite = _sprites[0];

        _parameterBar = FindObjectOfType<ParameterBar>();
        _characterParameterInfo = FindObjectOfType<CharacterParameterInfo>();

        StartCoroutine(DecideCorutine());
        _characterParamter.modelType = CharacterParameter.ModelType.HUMAN;
        _characterParamter.costumeType = CharacterParameter.CostumeType.A;
        _characterParamter.decorationType = CharacterParameter.DecorationType.A;
    }

    IEnumerator DecideCorutine()
    {
        yield return new WaitForSeconds(0.1f);

        _characterParamter.attack = 0;
        _characterParamter.defense = 0;
        _characterParamter.speed = 0;
        foreach (var parameter in FindObjectsOfType<ModelParameterInfo>())
        {
            _characterParamter.attack += parameter.getModelParameter.attack;
            _characterParamter.defense += parameter.getModelParameter.defence;
            _characterParamter.speed += parameter.getModelParameter.speed;
        }
        _characterParameterInfo.Decide();
        _parameterBar.ChangeParameterGauge();

        yield return null;
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(0.2f);

        // 遷移処理
    }
}
