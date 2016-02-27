using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
2 / 26
19 : 14 野本　変更開始

    後で中身の処理をいろいろと追加すると
    思ったので関数をButton１つにつき作りました

    ただButtonが１０個などになると
    汎用性が低いことが響くのでそこのところのアドバイスをお願いします

*/


public class ChangeCharacterPattern : MonoBehaviour
{
    [SerializeField]
    Image image = null;
    //image.color = new Color(1.0f, 0.0f, 0.0f);
    [SerializeField]
    GameObject[] panels = null;


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

    /*
    ///////TypeSelect時の処理
    */

    //TypeButtonのButton1を押したら
    public void PushTypeButtonOfButton1()
    {
        image.color = new Color(1.0f, 0.0f, 0.0f);
    }
    //TypeButtonのButton2を押したら
    public void PushTypeButtonOfButton2()
    {
        image.color = new Color(0.0f, 1.0f, 0.0f);
    }
    //TypeButtonのButton3を押したら
    public void PushTypeButtonOfButton3()
    {
        image.color = new Color(0.0f, 0.0f, 1.0f);
    }



    /*
    ///////CostumeSelect時の処理//////
    */

    //CostumeButtonのButton1を押したら
    public void PushCostumeButtonOfButton1()
    {
        image.transform.localScale = new Vector3(1.0f, 1.0f,1.0f);
    }
    //CostumeButtonのButton2を押したら
    public void PushCostumeButtonOfButton2()
    {
        image.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
    }
    //CostumeButtonのButton3を押したら
    public void PushCostumeButtonOfButton3()
    {
        image.transform.localScale = new Vector3(0.25f, 0.25f, 1.0f);
    }

    /*
    ///////DecorationSelect時の処理//////
    */

    //DecorationButtonのButton1を押したら
    public void PushDecorationButtonOfButton1()
    {
        image.transform.localPosition = new Vector3(1.0f, 1.0f, 1.0f);
    }
    //DecorationButtonのButton2を押したら
    public void PushDecorationButtonOfButton2()
    {
        image.transform.localPosition = new Vector3(0.5f, 0.5f, 1.0f);
    }
    //DecorationButtonのButton3を押したら
    public void PushDecorationButtonOfButton3()
    {
        image.transform.localPosition = new Vector3(0.25f, 0.25f, 1.0f);
    }


}
