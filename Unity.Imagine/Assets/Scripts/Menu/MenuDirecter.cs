using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    const string LOAD_SCENE_NAME = "Create";

    [SerializeField]
    Camera _camera = null;

    private bool _isChangeSelectGame = false;

    public List<Action> _ListsOfActionPushButton = new List<Action>();

    //今どのゲームを選択しているか
    private int _nowSelectGameNum = 1;

    List<Sprite> _sprites = new List<Sprite>();

    [SerializeField]
    Image _explanationImage = null;

    //現在のカメラRotation
    private int _nowCameraRotation = 0;
    private int _rotationSpeed = 3;


    //カメラ移動しているかどうか
    private bool _isChangingCameraRotation = false;


    void Start()
    {
        Register();
        _sprites.Add(Resources.Load<Sprite>("Menu/Texture/menu_title_power_game"));
        _sprites.Add(Resources.Load<Sprite>("Menu/Texture/menu_title_defence_game"));
        _sprites.Add(Resources.Load<Sprite>("Menu/Texture/menu_title_speed_game"));

        _explanationImage.sprite = _sprites[0];
    }

    private void Register()
    {
        _ListsOfActionPushButton.Add(() => 
        {
            //Createに戻す
            _isChangeSelectGame = false;

            _rotationSpeed = -3;
            _isChangingCameraRotation = true;
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
        if (_isChangingCameraRotation != true) return;
        _nowCameraRotation += _rotationSpeed;

        _camera.transform.localRotation =
             Quaternion.Euler(_nowCameraRotation, 0, 0);

        if (UnityEngine.Mathf.Abs(_nowCameraRotation) >= 90 && _rotationSpeed > 0)
        {
            FindObjectOfType<AnimeterTest>().isPlay = true;
            _isChangingCameraRotation = false;

        }

        else if (UnityEngine.Mathf.Abs(_nowCameraRotation) <= 0 && _rotationSpeed < 0)
        {
            _isChangingCameraRotation = false;
        }
    }

    public void PushOfCharaCreate()
    {
        //Createに移動
        var screenSequencer = ScreenSequencer.instance;

        if (screenSequencer.isEffectPlaying) return;

        screenSequencer.SequenceStart
            (
                () => { SceneManager.LoadScene(LOAD_SCENE_NAME); },
                new Fade(1.0f)
            );
    }

    public void PushOfMainGame()
    {
        if ( _isChangeSelectGame == false)
        {
            _isChangeSelectGame = true;

            _rotationSpeed = 3;
            _isChangingCameraRotation = true;

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
            _explanationImage.sprite = _sprites[_nowSelectGameNum - 1];
        }
        else if (nowSelectGameNum_ == 0)
        {
            _nowSelectGameNum = UnityEngine.Random.Range(1, 3);
            _explanationImage.sprite = _sprites[_nowSelectGameNum - 1];
        }
    }
}
