using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/*
    3 / 8 野本
    変更開始
    仕様
    
    最初2つの箱から１つ選ぶ
    クリエイト・ミニゲーム

    クリエイトの場合
    Scene移行

    ミニゲームの場合アニメーションからの
    6個のマスから選ぶ
    Button6個で操作
    ステージを選んでから、はいといいえを選択できる。
*/
public class MenuDirecter : MonoBehaviour
{
    [SerializeField]
    Camera _camera = null;

    private bool _isChangeSelectGame = false;

    public List<Action> _ListsOfActionPushButton = new List<Action>();

    //今どのゲームを選択しているか
    private int _nowSelectGameNum = 1;

    List<Sprite> _sprites = new List<Sprite>();

    [SerializeField]
    Image _explanationImage = null;

    void Start()
    {
        Register();
        _sprites.AddRange
       (
           Resources.LoadAll<Sprite>("MakeOfCharacter/Texture/Description")
       );
        _explanationImage.sprite = _sprites[0];
    }

    private void Register()
    {
        _ListsOfActionPushButton.Add(() => 
        {
            //Createに戻す
            _isChangeSelectGame = false;
            _camera.transform.localRotation =
                Quaternion.Euler(0, 0, 0);
        });

        _ListsOfActionPushButton.Add(() =>
        {
            //選択のはい
        });

    }

    void Update()
    {
        ChangeCameraAngle();
    }

    private void ChangeCameraAngle()
    {
        
    }

    public void PushOfCharaCreate()
    {
        //Createに移動
    }

    public void PushOfMainGame()
    {
        if ( _isChangeSelectGame == false)
        {
            _isChangeSelectGame = true;
            //TODO : ムービングを作る
            _camera.transform.localRotation =
                Quaternion.Euler(90, 0, 0);
            FindObjectOfType<AnimeterTest>().isPlay = true;
        }
    }

    public void ActionOfPushButton(int _buttonNum)
    {
        _ListsOfActionPushButton[_buttonNum]();
    }

    public void SelectOfGameNum(int nowSelectGameNum_)
    {
        if (nowSelectGameNum_ >= 1 && nowSelectGameNum_ <= 3)
        {
            _nowSelectGameNum = nowSelectGameNum_;
            Debug.Log(_nowSelectGameNum);
            _explanationImage.sprite = _sprites[_nowSelectGameNum];
        }
        else if (nowSelectGameNum_ == 0)
        {
            _nowSelectGameNum = UnityEngine.Random.Range(1, 3);
            _explanationImage.sprite = _sprites[_nowSelectGameNum];
        }
    }

}
