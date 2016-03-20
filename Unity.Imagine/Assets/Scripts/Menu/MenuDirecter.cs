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

    [SerializeField]
    Image _explanationImage = null;

    //現在のカメラRotation
    private float _nowCameraRotation = 0;
    private float _rotationSpeed = 1.5f / 10;

    private bool _canSelectGame = true;

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
    private Vector3 _defAngle;

    private double _totalReverseAnimationCount = 0.0f;
    private double _reverseAnimationCount = 0.0f;
    private bool _isChangedAnimationActive = false;

    [SerializeField]
    GameObject _animation = null;

    [SerializeField]
    GameObject _statusCursor = null;
    enum NowCameraMode
    {
        NONE,
        UP_ANGLE,
        DOWN_ANGLE
    }

    private NowCameraMode _nowCameraMode = NowCameraMode.NONE;

    [SerializeField]
    Image _selectGameName = null;

    private List<Sprite> _gameNames = new List<Sprite>();

    void Start()
    {
        Register();

        ChangeStatusCursor(_nowSelectGameNum);

        for (int i = 0; i < 2; ++i)
        {
            _startPosition[i] = new Vector3(_characterAnimation[i].transform.localPosition.x,
                                            _characterAnimation[i].transform.localPosition.y,
                                            _characterAnimation[i].transform.localPosition.z);

            _def[i] = new Vector3(_animationStop[i].transform.localPosition.x - _startPosition[i].x,
                                  _animationStop[i].transform.localPosition.y - _startPosition[i].y,
                                  _animationStop[i].transform.localPosition.z - _startPosition[i].z);
        }

        _defAngle = _animationStop[1].transform.localEulerAngles;
    }

    private void Register()
    {
        _ListsOfActionPushButton.Add(() =>
        {
            if (_nowCameraMode != NowCameraMode.NONE) return;
            _nowCameraMode = NowCameraMode.UP_ANGLE;
            _animationCount = 1.0f;
            _reverseAnimationCount = FindObjectOfType<MenuBoxAnimater>().animationTime;
            _totalReverseAnimationCount = _reverseAnimationCount;
            FindObjectOfType<MenuBoxAnimater>().isBack = true;
            _isChangedAnimationActive = false;
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
        _ListsOfActionPushButton.Add(() =>
        {
            FindObjectOfType<ChangeText>().ChangeExplanationText(5);
        });


        _gameNames.Add(Resources.Load<Sprite>("Menu/Texture/menu_title"));
        _gameNames.Add(Resources.Load<Sprite>("Menu/Texture/menu_title1"));
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

        if (TouchController.IsTouchBegan() && _canMoveCharacter == false && _canSelectGame == true)
            TouchCharacter();
    }

    private void TouchCharacter()
    {
        var hitObject = new RaycastHit();
        var isHit = TouchController.IsRaycastHit(out hitObject);

        for (int i = 0; i < 2; ++i)
            if (hitObject.transform.name == _characterAnimation[i].name)
            {
                if (_characterAnimation[i].name == "Asobu")
                {
                    _canMoveCharacter = true;
                    _canSelectGame = false;
                    _nowCameraMode = NowCameraMode.DOWN_ANGLE;
                    _animationCount = 0.0f;
                    FindObjectOfType<ChangeText>().ChangeExplanationText(0);
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
        if (_totalReverseAnimationCount > _reverseAnimationCount + 3.0f)
        {
            while (_nowCameraRotation > 0.0f)
            {
                _nowCameraRotation -= Time.deltaTime * 30;

                _camera.transform.localRotation =
                     Quaternion.Euler(_nowCameraRotation, 0, 0);
                yield return null;
            }
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

            _characterAnimation[1].transform.localRotation
                = Quaternion.Euler(_defAngle.x * _animationCount,
                                   _defAngle.y * _animationCount,
                                   _defAngle.z * _animationCount);

            yield return null;
        }

        for (int i = 0; i < 2; ++i)
        {
            _characterAnimation[i].transform.localPosition
                    = new Vector3(_animationStop[i].transform.localPosition.x,
                                  _animationStop[i].transform.localPosition.y,
                                  _animationStop[i].transform.localPosition.z);
        }

        //Test
        _characterAnimation[1].transform.localRotation = Quaternion.Euler(60, 105, 110);

        _nowCameraMode = NowCameraMode.NONE;
        _animation.SetActive(true);
        FindObjectOfType<MenuBoxAnimater>().isPlay = true;
        _characterAnimation[0].SetActive(false);
        _canMoveCharacter = false;
        yield return null;
    }

    IEnumerator EndDirection()
    {
        _totalReverseAnimationCount += Time.deltaTime;

        if (_totalReverseAnimationCount > _reverseAnimationCount + 3.0f)
        {
            if (_isChangedAnimationActive == false)
            {
                _animation.SetActive(false);
                _characterAnimation[0].SetActive(true);
                _isChangedAnimationActive = true;
            }

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

                _characterAnimation[1].transform.localRotation
                              = Quaternion.Euler(_defAngle.x * _animationCount,
                                                 _defAngle.y * _animationCount,
                                                 _defAngle.z * _animationCount);

                yield return null;
            }

            for (int i = 0; i < 2; ++i)
            {
                _characterAnimation[i].transform.localPosition
                    = new Vector3(_startPosition[i].x,
                                  _startPosition[i].y,
                                  _startPosition[i].z);
            }

            //Test
            _characterAnimation[1].transform.localRotation = Quaternion.Euler(0, 0, 0);
            _nowCameraMode = NowCameraMode.NONE;
            _canMoveCharacter = false;
            _canSelectGame = true;
            yield return null;
        }
    }

    public void ActionOfPushButton(int _buttonNum)
    {
        if (_canMoveCharacter == false)
            _ListsOfActionPushButton[_buttonNum]();
    }

    public void SelectOfGameNum(int nowSelectGameNum_)
    {
        if (nowSelectGameNum_ >= 0 && nowSelectGameNum_ <= 2 && _canMoveCharacter == false)
        {
            _nowSelectGameNum = nowSelectGameNum_;
            Debug.Log(_nowSelectGameNum);
            FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
            FindObjectOfType<ChangeTarget>().ChangeTargetCursor(_nowSelectGameNum);
            FindObjectOfType<ChangeText>().ChangeExplanationText(1 + _nowSelectGameNum);
            ChangeStatusCursor(_nowSelectGameNum);
            
        }
        else if (nowSelectGameNum_ == 3 && _canMoveCharacter == false)
        {
            _nowSelectGameNum = UnityEngine.Random.Range(0, 3);
            FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
            FindObjectOfType<ChangeTarget>().ChangeTargetCursor(_nowSelectGameNum);
            FindObjectOfType<ChangeText>().ChangeExplanationText(4);
            ChangeStatusCursor(_nowSelectGameNum);
        }
        if (_nowSelectGameNum == 0)
            _selectGameName.sprite = _gameNames[1];
        else
            _selectGameName.sprite = _gameNames[0];
    }

    private void ChangeStatusCursor(int _selectGameNum)
    {
        _statusCursor.transform.localRotation = Quaternion.Euler(0, 0, (_selectGameNum * -120) + 240);
    }
}
