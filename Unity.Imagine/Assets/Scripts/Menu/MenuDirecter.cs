using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;

public class MenuDirecter : MonoBehaviour
{
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
    private float _nowCameraRotation = 0;
    private float _rotationSpeed = 1.5f / 10;

    //カメラ移動しているかどうか
    private bool _isChangingCameraRotation = false;

    //Characterの移動アニメーションをしていいかどうか
    private bool _canMoveCharacter = false;

    [SerializeField]
    GameObject[] _characterAnimation = null;

    [SerializeField]
    Transform[] _animationStop = null;

    private Vector3[] _startPosition = new Vector3[2];

    private float _animationCount = 0.0f;
    private Vector3[] _def = new Vector3[2];

    enum NowCameraMode
    {
        NONE,
        UP_ANGLE,
        DOWN_ANGLE
    }

    private NowCameraMode _nowCameraMode = NowCameraMode.NONE;

    void Start()
    {
        Register();
        _sprites.Add(Resources.Load<Sprite>("Menu/Texture/menu_title_power_game"));
        _sprites.Add(Resources.Load<Sprite>("Menu/Texture/menu_title_defence_game"));
        _sprites.Add(Resources.Load<Sprite>("Menu/Texture/menu_title_speed_game"));

        _explanationImage.sprite = _sprites[0];

        for (int i = 0; i < 2; ++i)
        {
            _startPosition[i] = new Vector3(_characterAnimation[i].transform.localPosition.x,
                                            _characterAnimation[i].transform.localPosition.y,
                                            _characterAnimation[i].transform.localPosition.z);

            _def[i] = new Vector3(_animationStop[i].transform.localPosition.x - _startPosition[i].x,
                                  _animationStop[i].transform.localPosition.y - _startPosition[i].y,
                                  _animationStop[i].transform.localPosition.z - _startPosition[i].z);
        }
    }

    private void Register()
    {
        _ListsOfActionPushButton.Add(() =>
        {
            _nowCameraMode = NowCameraMode.UP_ANGLE;
            _animationCount = 1.0f;
            FindObjectOfType<MenuBoxAnimater>().isBack = true;
        });

        _ListsOfActionPushButton.Add(() =>
        {
            //選択のはい
            var screenSequencer = ScreenSequencer.instance;

            if (screenSequencer.isEffectPlaying) return;

            screenSequencer.SequenceStart
                (
                    () => { GameScene.MiniGames.ChangeScene(); },
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
                    () => { GameScene.Title.ChangeScene(); },
                    new Fade(1.0f)
                );
        });
    }

    void Update()
    {
        if (_nowCameraMode == NowCameraMode.DOWN_ANGLE)
        {
            StartCoroutine(StartDirection());
            StartCoroutine(ChangeStartCameraAngle());
        }

        else if (_nowCameraMode == NowCameraMode.UP_ANGLE)
        {
            StartCoroutine(EndDirection());
            StartCoroutine(ChangeEndCameraAngle());
        }

        if (TouchController.IsTouchBegan() && _canMoveCharacter == false)
            TouchCharacter();
    }

    private void TouchCharacter()
    {
        var hitObject = new RaycastHit();
        var isHit = TouchController.IsRaycastHit(out hitObject);

        for (int i = 0; i < 2; ++i)
            if (hitObject.transform.name == _characterAnimation[i].name)
            {
                if (_characterAnimation[i].name == "Asobu" && _canMoveCharacter == false)
                {
                    _canMoveCharacter = true;
                    _nowCameraMode = NowCameraMode.DOWN_ANGLE;
                    _animationCount = 0.0f;
                }

                else if (_characterAnimation[i].name == "Tukuru")
                {
                    //Createに移動
                    var screenSequencer = ScreenSequencer.instance;

                    if (screenSequencer.isEffectPlaying) return;

                    screenSequencer.SequenceStart
                        (
                            () => { GameScene.Create.ChangeScene(); },
                            new Fade(1.0f)
                        );
                }
            }
    }

    IEnumerator ChangeStartCameraAngle()
    {
        while (_nowCameraRotation < 90.0f)
        {
            _nowCameraRotation += Time.deltaTime * 30;

            _camera.transform.localRotation =
                 Quaternion.Euler(_nowCameraRotation, 0, 0);

            yield return null;
        }
       
    }

    IEnumerator ChangeEndCameraAngle()
    {
        while (_nowCameraRotation > 0.0f)
        {
            _nowCameraRotation -= Time.deltaTime * 30;

            _camera.transform.localRotation =
                 Quaternion.Euler(_nowCameraRotation, 0, 0);
            yield return null;
        }
    }

    IEnumerator StartDirection()
    {
        // 遊びオブジェクトの移動処理
        _animationCount += Time.deltaTime;
        while (_animationCount <= 1.0f)
        {
            for (int i = 0; i < 2; ++i)
            {
                _characterAnimation[i].transform.localPosition
                       = new Vector3(_startPosition[i].x + _def[i].x * _animationCount,
                                     _startPosition[i].y + _def[i].y * _animationCount,
                                     _startPosition[i].z + _def[i].z * _animationCount);
            }
            yield return null;
        }

        for (int i = 0; i < 2; ++i)
        {
            _characterAnimation[i].transform.localPosition
                    = new Vector3(_animationStop[i].transform.localPosition.x,
                                  _animationStop[i].transform.localPosition.y,
                                  _animationStop[i].transform.localPosition.z);
        }
            _nowCameraMode = NowCameraMode.NONE;
        FindObjectOfType<MenuBoxAnimater>().isPlay = true;

        yield return null;
    }

    IEnumerator EndDirection()
    {
       

        // 遊びオブジェクトの移動処理
        _animationCount -= Time.deltaTime;

        while (_animationCount > 0.0f)
        {
            for (int i = 0; i < 2; ++i)
            {
                _characterAnimation[i].transform.localPosition
                       = new Vector3(_startPosition[i].x + _def[i].x * _animationCount,
                                     _startPosition[i].y + _def[i].y * _animationCount,
                                     _startPosition[i].z + _def[i].z * _animationCount);
            }
            yield return null;
        }

        for (int i = 0; i < 2; ++i)
        {
            _characterAnimation[i].transform.localPosition
                = new Vector3(_startPosition[i].x,
                              _startPosition[i].y,
                              _startPosition[i].z);
        }

        _nowCameraMode = NowCameraMode.NONE;
        _canMoveCharacter = false;

        yield return null;
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
            FindObjectOfType<ChangeTarget>().ChangeTargetCursor(_nowSelectGameNum);
        }
        else if (nowSelectGameNum_ == 3)
        {
            _nowSelectGameNum = UnityEngine.Random.Range(0, 3);
            _explanationImage.sprite = _sprites[_nowSelectGameNum];
            FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
            FindObjectOfType<ChangeTarget>().ChangeTargetCursor(_nowSelectGameNum);
        }
    }
}
