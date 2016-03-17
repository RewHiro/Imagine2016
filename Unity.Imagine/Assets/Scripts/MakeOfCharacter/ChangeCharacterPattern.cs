using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    GameObject _characterPlace = null;

    [SerializeField]
    Image _description = null;

    [SerializeField]
    Transform _panelOfChangeType = null;

    [SerializeField]
    Transform _panelOfChangeCostume = null;

    [SerializeField]
    Transform _panelOfChangeDecoration = null;

    List<Sprite> _descriptionSprites = new List<Sprite>();
    List<Texture> _characterTextures = new List<Texture>();

    GameObject _character = null;

    CharacterParameter _characterParamter = new CharacterParameter();
    ParameterBar _parameterBar = null;
    CharacterParameterInfo _characterParameterInfo = null;

    static bool _isPush = false;

    public CharacterParameter getCharacterParamter
    {
        get
        {
            return _characterParamter;
        }
    }

    public void Decide()
    {
        if (ScreenSequencer.instance.isEffectPlaying) return;
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

    //
    public void PushOfBackTitle()
    {
        //右上のButtonを押したら

        var screenSequencer = ScreenSequencer.instance;

        if (screenSequencer.isEffectPlaying) return;

        screenSequencer.SequenceStart
        (
            () => { GameScene.Menu.ChangeScene(); },
            new Fade(1.0f)
        );
    }

    public void SetType(int index)
    {
        if ((uint)index > (int)CharacterParameter.ModelType.NONE) throw new ArgumentException("type is error");

        if (_isPush) return;

        ChangeSelect(index, (int)getCharacterParamter.modelType, _panelOfChangeType);

        _character = CreateModel((uint)index, _typePrefabs, _character.transform.parent);
        SetCostume((int)getCharacterParamter.costumeType);

        _characterParamter.modelType = (CharacterParameter.ModelType)index;

        SetDecoration((int)_characterParamter.decorationType);

        _description.sprite = _descriptionSprites[index];

        _character.transform.GetChild(0).GetComponentInChildren<CharacterAppearance>().enabled = 
            false;

        StartCoroutine(DecideCorutine());
    }

    public void SetCostume(int index)
    {
        if ((uint)index > (int)CharacterParameter.CostumeType.NONE) throw new ArgumentException("costume is error");

        if (_isPush) return;

        ChangeSelect(index, (int)getCharacterParamter.costumeType, _panelOfChangeCostume);

        var place = _character.transform.GetChild(0);
        CreateModel((uint)index, _costumePrefabs, place);

        _characterParamter.costumeType = (CharacterParameter.CostumeType)index;
        _description.sprite = _descriptionSprites[index + 3];
        StartCoroutine(DecideCorutine());
    }

    public void SetDecoration(int index)
    {
        if ((uint)index > (int)CharacterParameter.DecorationType.C) throw new ArgumentException("type is error");

        if (_isPush) return;

        ChangeSelect(index, (int)getCharacterParamter.decorationType, _panelOfChangeDecoration);

        _characterParamter.decorationType = (CharacterParameter.DecorationType)index;
        _character.GetComponent<MeshRenderer>().material.mainTexture = _characterTextures[index + (int)_characterParamter.modelType * 4];
        _description.sprite = _descriptionSprites[index + 6];
        StartCoroutine(DecideCorutine());
    }

    GameObject CreateModel(uint index, GameObject[] prefabs, Transform parent)
    {
        if (index > prefabs.Length) throw new IndexOutOfRangeException("out of range");

        var child = parent.GetChild(0);
        if (child.gameObject == null) throw new NullReferenceException(parent.name + " is nothing child");
        Destroy(child.gameObject);

        var model = Instantiate(prefabs[index]);
        model.name = prefabs[index].name;
        model.transform.SetParent(parent, false);
        model.transform.localRotation = Quaternion.identity;

        return model;
    }

    void ChangeSelect(int index, int type, Transform panel)
    {
        if (index == type) return;
        Destroy(panel.GetChild(type).GetChild(0).gameObject);
        var circle = Instantiate(Resources.Load<GameObject>("MakeOfCharacter/TypeSelect"));
        circle.transform.SetParent(panel.GetChild(index), false);
    }

    void Start()
    {
        _descriptionSprites.AddRange
        (
            Resources.LoadAll<Sprite>("MakeOfCharacter/Texture/Description")
        );

        _characterTextures.AddRange
            (
                Resources.LoadAll<Texture>("Character/Beast/Texture")
            );

        _characterTextures.AddRange
            (
                Resources.LoadAll<Texture>("Character/Human/Texture")
            );

        _characterTextures.AddRange
            (
                Resources.LoadAll<Texture>("Character/Robo/Texture")
            );

        _character = _characterPlace.transform.GetChild(0).gameObject;

        _description.sprite = _descriptionSprites[0];

        _parameterBar = FindObjectOfType<ParameterBar>();
        _characterParameterInfo = FindObjectOfType<CharacterParameterInfo>();

        StartCoroutine(DecideCorutine());
        _characterParamter.modelType = CharacterParameter.ModelType.HUMAN;
        _characterParamter.costumeType = CharacterParameter.CostumeType.A;
        _characterParamter.decorationType = CharacterParameter.DecorationType.NONE;
    }

    IEnumerator DecideCorutine()
    {
        if (_isPush) yield break;
        _isPush = true;

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

        _isPush = false;

        yield return null;
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(0.2f);

        // 遷移処理

        ScreenSequencer.instance.SequenceStart
            (
                () => { GameScene.Printer.ChangeScene(); },
                new Fade(1.0f)
            );
    }
}
