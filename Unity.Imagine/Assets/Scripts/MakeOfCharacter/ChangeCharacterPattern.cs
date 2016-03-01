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
    public Image image = null;
    [SerializeField]
    public GameObject[] panels = null;

    public List<Action> _listOfPushButtonAction = new List<Action>();

    void Start()
    {
        Register();
    }

    //ラッピング
    public void Register()
    {
        _listOfPushButtonAction.Add(() => { image.color = new Color(1.0f, 0.0f, 0.0f); });
        _listOfPushButtonAction.Add(() => { image.color = new Color(0.0f, 1.0f, 0.0f); });
        _listOfPushButtonAction.Add(() => { image.color = new Color(0.0f, 0.0f, 1.0f); });
        _listOfPushButtonAction.Add(() => { image.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); });
        _listOfPushButtonAction.Add(() => { image.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f); });
        _listOfPushButtonAction.Add(() => { image.transform.localScale = new Vector3(0.25f, 0.25f, 1.0f); });
        _listOfPushButtonAction.Add(() => { image.transform.localPosition = new Vector3(1.0f, 1.0f, 1.0f); });
        _listOfPushButtonAction.Add(() => { image.transform.localPosition = new Vector3(5.0f, 5.0f, 5.0f); });
        _listOfPushButtonAction.Add(() => { image.transform.localPosition = new Vector3(10.0f, 10.0f, 10.0f); });
    }


    //TypeButtonを押したら
    public void PushButtonOfType()
    {
        panels[0].SetActive(true);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
    }

    //CostumeButtonを押したら
    public void PushButtonOfCostume()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
        panels[2].SetActive(false);
    }

    //DecorationButtonを押したら
    public void PushButtonOfDecoration()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(false);
        panels[2].SetActive(true);
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
