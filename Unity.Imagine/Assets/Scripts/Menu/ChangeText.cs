using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

public class ChangeText : MonoBehaviour
{

    private List<String> _text = new List<string>();
   
    void Start()
    {
        _text.Add("このがめんでは\nゲームをえらべるよ\n3つのゲームからえらんでね");
        _text.Add("このゲームはおたがいのぼうしを\nねらってたまをうちあうゲームだよ。\nはやいCuboがとくいだよ\nよかったらOKをおしてね");
        _text.Add("このゲームは\nまだあそべないよ");
        _text.Add("このゲームは\nまだあそべないよ");
        _text.Add("ゲームをじどうでえらんでるよ");
        _text.Add("これはゲームをえらんだときに\nどんなCuboがつよいのかみれるよ。");

        ChangeExplanationText(0);
    }

   
    void Update()
    {

    }


    public void ChangeExplanationText(int _selectNum)
    {
        this.GetComponent<Text>().text = _text[_selectNum];
    }
}
