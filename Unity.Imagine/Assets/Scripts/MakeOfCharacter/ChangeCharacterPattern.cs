using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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
    public GameObject _image = null;
    [SerializeField]
    public GameObject[] _panels = null;

    //Characterの全体像
    [SerializeField]
    GameObject _characterPlace = null;

    //Typeのモデル
    [SerializeField]
    GameObject[] _modelPrefabs = null;

    //Costumeのモデル
    [SerializeField]
    GameObject[] _costumePrefabs = null;

    //Decorationのモデル
    [SerializeField]
    GameObject[] _decorationPrefabs = null;

    public List<Action> _listOfPushButtonAction = new List<Action>();

    void Start()
    {
        Register();
    }

    //ラッピング
    public void Register()
    {

        ////Type///////////////////////////////////////////////////////
        _listOfPushButtonAction.Add(() => 
        {
            var _character = GameObject.FindGameObjectWithTag("Character");
            Destroy(_character);
            var model = Instantiate(_modelPrefabs[0]);
            model.transform.SetParent(_characterPlace.transform);
            model.transform.localPosition = Vector3.zero;
        });

        _listOfPushButtonAction.Add(() => 
        {
            var _character = GameObject.FindGameObjectWithTag("Character");
            Destroy(_character);
            var model = Instantiate(_modelPrefabs[1]);
            model.transform.SetParent(_characterPlace.transform);
            model.transform.localPosition = Vector3.zero;
        });

        _listOfPushButtonAction.Add(() => 
        {
            var _character = GameObject.FindGameObjectWithTag("Character");
            Destroy(_character);
            var model = Instantiate(_modelPrefabs[2]);
            model.transform.SetParent(_characterPlace.transform);
            model.transform.localPosition = Vector3.zero;
        });

        ////Costume///////////////////////////////////////////////////////

        _listOfPushButtonAction.Add(() => 
        {
            var _costume = GameObject.FindGameObjectWithTag("Costume");
            Destroy(_costume);
            var model = Instantiate(_costumePrefabs[0]);
            model.transform.SetParent(_characterPlace.transform);
            model.transform.localPosition = new Vector3(0,0.45f, 0);
        });
        _listOfPushButtonAction.Add(() => 
        {
            var _costume = GameObject.FindGameObjectWithTag("Costume");
            Destroy(_costume);
            var model = Instantiate(_costumePrefabs[1]);
            model.transform.SetParent(_characterPlace.transform);
            model.transform.localPosition = new Vector3(0,0.45f, 0);
        });
        _listOfPushButtonAction.Add(() => 
        {
            var _costume = GameObject.FindGameObjectWithTag("Costume");
            Destroy(_costume);
            var model = Instantiate(_costumePrefabs[2]);
            model.transform.SetParent(_characterPlace.transform);
            model.transform.localPosition = new Vector3(0,0.45f, 0);
        });


        ////Decoration///////////////////////////////////////////////////////

        _listOfPushButtonAction.Add(() => 
        {
            var _decoration = GameObject.FindGameObjectWithTag("Decoration");
            Destroy(_decoration);
            var model = Instantiate(_decorationPrefabs[0]);
            model.transform.SetParent(_characterPlace.transform);
            model.transform.localPosition = new Vector3(0.35f, 0, 0);
        });
        _listOfPushButtonAction.Add(() => 
        {
            var _decoration = GameObject.FindGameObjectWithTag("Decoration");
            Destroy(_decoration);
            var model = Instantiate(_decorationPrefabs[1]);
            model.transform.SetParent(_characterPlace.transform);
            model.transform.localPosition = new Vector3(0.35f, 0, 0);
        });
        _listOfPushButtonAction.Add(() => 
        {
            var _decoration = GameObject.FindGameObjectWithTag("Decoration");
            Destroy(_decoration);
            var model = Instantiate(_decorationPrefabs[2]);
            model.transform.SetParent(_characterPlace.transform);
            model.transform.localPosition = new Vector3(0.35f, 0, 0);
        });


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

    public void ActionOfPushButton(int buttonNum_)
    {
        //3 * 3なので 0~8まで
        if (buttonNum_ >= 0 && buttonNum_ <= 8)
            _listOfPushButtonAction[buttonNum_]();

        //もし踏み外したら
        else if (buttonNum_ >= 9)
            Debug.Log(buttonNum_);
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


}
