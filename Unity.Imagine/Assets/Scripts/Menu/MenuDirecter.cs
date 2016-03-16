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
    const string LOAD_SCENE_CREATE = "Create";
    const string LOAD_SCENE_TITLE = "Title";
    const string LOAD_SCENE_GAME = "MiniGames";

    [SerializeField]
    Camera _camera = null;

    private bool _isChangeSelectGame = false;

    public List<Action> _ListsOfActionPushButton = new List<Action>();

    //今どのゲームを選択しているか
    private int _nowSelectGameNum = 0;

    List<Sprite> _sprites = new List<Sprite>();

    [SerializeField]
    Image _explanationImage = null;

    //現在のカメラRotation
    private int _nowCameraRotation = 0;
    private int _rotationSpeed = 3;


    //カメラ移動しているかどうか
    private bool _isChangingCameraRotation = false;

    //TargetCursor
    [SerializeField]
    Image _targetCursor = null;

    void Start()
    {
        Register();
        _sprites.Add(Resources.Load<Sprite>("Menu/Texture/menu_title_power_game"));
        _sprites.Add(Resources.Load<Sprite>("Menu/Texture/menu_title_defence_game"));
        _sprites.Add(Resources.Load<Sprite>("Menu/Texture/menu_title_speed_game"));

        _explanationImage.sprite = _sprites[0];
        MoveCursor(_nowSelectGameNum);
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
            var screenSequencer = ScreenSequencer.instance;

            if (screenSequencer.isEffectPlaying) return;

            screenSequencer.SequenceStart
                (
                    () => { SceneManager.LoadScene(LOAD_SCENE_GAME); },
                    new Fade(1.0f)
                );
        });

        _ListsOfActionPushButton.Add(() =>
        {
            //Titleに移動
            var screenSequencer = ScreenSequencer.instance;

            if (screenSequencer.isEffectPlaying) return;

            screenSequencer.SequenceStart
                (
                    () => { SceneManager.LoadScene(LOAD_SCENE_TITLE); },
                    new Fade(1.0f)
                );
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
                () => { SceneManager.LoadScene(LOAD_SCENE_CREATE); },
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
        if (nowSelectGameNum_ >= 0 && nowSelectGameNum_ <= 2)
        {
            _nowSelectGameNum = nowSelectGameNum_;
            Debug.Log(_nowSelectGameNum);
            _explanationImage.sprite = _sprites[_nowSelectGameNum];
            FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
            MoveCursor(_nowSelectGameNum);
        }
        else if (nowSelectGameNum_ == 3)
        {
            _nowSelectGameNum = UnityEngine.Random.Range(0, 2);
            _explanationImage.sprite = _sprites[_nowSelectGameNum];
            FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
           MoveCursor(_nowSelectGameNum);
        }
    }

    private void MoveCursor(int _selectGameNum)
    {
        _targetCursor.transform.localPosition
            = new Vector3(-460 + _selectGameNum * 200,-40,-450);
    }

}
