using UnityEngine;
using System.Collections;

public class TitleDirecter : MonoBehaviour {

    [SerializeField]
    GameObject _logo = null;
    //Easingで掛ける距離
    private float _s = 1.70158f * 1.525f;
    //Eaisngしている合計時間
    private float _easingTime = 0.0f;
    //Easing関数
    private float EasingBackInOut(float time_, float beginPos_, float endPos_)
    {
        if ((time_ /= 0.5f) < 1.0f) return (endPos_ - beginPos_) / 2 * (time_ * time_ * ((_s + 1) * time_ - _s)) + beginPos_;
        time_ -= 2;
        return (endPos_ - beginPos_) / 2 * (time_ * time_ * ((_s + 1) * time_ + _s) + 2) + beginPos_;
    }

    //移動する雲
    [SerializeField]
    GameObject[] _cloud = null;
    //雲の移動速度
    private float _moveCloudSpeed = 0.4f * 10;

    //2体のCharacter
    [SerializeField]
    GameObject[] _characterView = null;
    //待っている時間
    private int[] _waitTime = new int[2];
    //最大待ち時間
    private int[] _totalWaitTime = new int[2];
    //飛んでいるかどうか
    private bool[] _isJump = new bool[2];
    //飛ぶ威力
    private int _jumpPower = 8;
    //ジャンプしている時間
    private int[] _jumpCount = new int[2];
    //重力
    private float _gravity = 0.1f;
    //_ジャンプ開始位置
    private float[] _tempPosY = new float[2];

    //こける処理
    //1回飛ぶごとにCountを＋１
    private int _kickCount = 0;
    //何回目でこかすか
    private int _totalKickCount = 5;
    //こけるSpeed
    private int[] _fallSpeed = new int[2];
    //こけているかどうか
    private bool[] _isFall = new bool[2];
    //こけている秒数
    private int[] _fallCount = new int[2];
    //こけている最大時間
    private int[] _totalFallCount = new int[2];

    void Start()
    {
        SetupOfCharacterView();
    }

    void SetupOfCharacterView()
    {
        for (int i = 0; i < _characterView.Length; ++i)
        {
            _jumpCount[i] = 0;
            _tempPosY[i] = _characterView[i].transform.localPosition.y;
            _isFall[i] = false;
            _fallCount[i] = 0;
            _totalFallCount[i] = 0;
            _totalWaitTime[i] = UnityEngine.Random.Range(60, 90);
        }
        _isJump[0] = false;
        _isJump[1] = true;
        _fallSpeed[0] = -6;
        _fallSpeed[1] = 6;
        _waitTime[0] = 30;
        _waitTime[1] = 0;
    }

    void Update()
    {
        //NameLogoのUpdate
        UpdateOfNameLogo();
        //雲のUpdate
        UpdateOfClouds();
        //CharacterのUpdate
        UpdateOfCharacterView();
    }


    private void UpdateOfNameLogo()
    {
        //Easingを40Countで行う
        if (_easingTime < 1.0f)
            _easingTime += 1.0f / 40.0f;
        //local座標の移動
        _logo.transform.localPosition = new Vector3(-460, EasingBackInOut(_easingTime, 1200, 400), 0);
    }


    private void UpdateOfClouds()
    {
        for (int i = 0; i < _cloud.Length; ++i)
        {
            //移動処理
            _cloud[i].transform.localPosition =
                new Vector3(_cloud[i].transform.localPosition.x + _moveCloudSpeed,
                            _cloud[i].transform.localPosition.y,
                            _cloud[i].transform.localPosition.z);

            //範囲の外に出たら左側に戻す
            if (_cloud[i].transform.localPosition.x > 1100)
            {
                _cloud[i].transform.localPosition =
                new Vector3(_cloud[i].transform.localPosition.x - 2400,
                            _cloud[i].transform.localPosition.y,
                            _cloud[i].transform.localPosition.z);
            }
        }
    }


    void UpdateOfCharacterView()
    {
        for (int i = 0; i < _characterView.Length; ++i)
        {
            if (_isJump[i] == false)
                ++_waitTime[i];
            if (_waitTime[i] >= _totalWaitTime[i])
            {
                //Randomで飛ぶタイミングを変更
                _totalWaitTime[i] = UnityEngine.Random.Range(60, 90);
                _isJump[i] = true;
                ++_kickCount;
                //Trueならこかす
                if (_kickCount % _totalKickCount == 0)
                    _isFall[i] = true;

                _waitTime[i] = 0;
            }

            if (_isJump[i] == true)
            {
                ++_jumpCount[i];

                _characterView[i].transform.localPosition
                    = new Vector3(_characterView[i].transform.localPosition.x,
                                  _characterView[i].transform.localPosition.y
                                + _jumpPower - _gravity * _jumpCount[i] * _jumpCount[i],
                                  _characterView[i].transform.localPosition.z);

                //こけていないときに回転させる
                if (_isFall[i] == false)
                {
                    _characterView[i].transform.Rotate(new Vector3(0, _fallSpeed[i] * 4, 0));
                }

                //初期位置より下にいったら元の位置に戻し、ジャンプ処理の終了
                if (_characterView[i].transform.localPosition.y < _tempPosY[i])
                {
                    _isJump[i] = false;
                    _characterView[i].transform.localPosition
                    = new Vector3(_characterView[i].transform.localPosition.x,
                                  _tempPosY[i],
                                  _characterView[i].transform.localPosition.z);
                    _jumpCount[i] = 0;
                }

            }
        }

        //Character転ぶ処理
        UpdateOfCharacterFall();

    }

    private void UpdateOfCharacterFall()
    {
        for (int i = 0; i < _characterView.Length; ++i)
        {
            if (_isFall[i] == true)
            {
                ++_fallCount[i];
                //Rotateをいじりこかす
                _characterView[i].transform.Rotate(new Vector3(0, 0, _fallSpeed[i]));
                //Count 90 / fallSpeed = 15なので　15基準です
                if (_fallCount[i] == 15)
                {
                    //起こすためにSpeedを-1
                    _fallSpeed[i] *= -1;
                    _isJump[i] = true;
                }

                //起きたら戻す
                if (_fallCount[i] >= 30)
                {
                    _isFall[i] = false;
                    _fallCount[i] = 0;
                }

            }
        }
    }
}
