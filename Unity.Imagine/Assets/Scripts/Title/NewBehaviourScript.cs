using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    GameObject _logo = null;
    //Easingで掛ける距離
    private float _s = 1.70158f * 1.525f;
    //Eaisngしている合計時間
    private float _easingTime = 0.0f;
    //雲の移動速度
    private float _moveCloudSpeed = 0.4f * 10;

    private float EasingBackInOut(float time_, float beginPos_, float endPos_)
    {
        if ((time_ /= 0.5f) < 1.0f) return (endPos_ - beginPos_) / 2 * (time_ * time_ * ((_s + 1) * time_ - _s)) + beginPos_;
        time_ -= 2;
        return (endPos_ - beginPos_) / 2 * (time_ * time_ * ((_s + 1) * time_ + _s) + 2) + beginPos_;
    }

    [SerializeField]
    GameObject[] _cloud = null;


    [SerializeField]
    GameObject[] _characterView = null;

    private int[] _waitTime = new int[2];
    private bool[] _isJump = new bool[2]; 
    private int _jumpPower = 8;
    private int[] _jumpCount = new int[2];
    private float _gravity = 0.1f;
    private float[] _tempPosY = new float[2];

    [SerializeField]
    GameObject _throwBall = null;

    private Vector2 _throwSpeed = new Vector2(2.8f, 0.5f);
    private int _throwCount = 0;
    private bool _isThrow = false;

    void Start ()
    {
	

        for(int i = 0; i < _characterView.Length; ++i)
        {
            _isJump[0] = false;
            _isJump[1] = true;
            _jumpCount[i] = 0;
            _waitTime[0] = 30;
            _waitTime[1] = 0;
            _tempPosY[i] = _characterView[i].transform.localPosition.y;
        }
	}
	
	void Update ()
    {
        UpdateOfNameLogo();
        UpdateOfClouds();
        UpdateOfCharacterView();
        UpdateOfThrowBall();
	}

    private void UpdateOfNameLogo()
    {
        if (_easingTime < 1.0f)
            _easingTime += 1.0f / 40.0f;

        _logo.transform.localPosition = new Vector3(-461, EasingBackInOut(_easingTime, 1200, 400), 0);
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
            if(_cloud[i].transform.localPosition.x > 1100)
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
            if(_isJump[i] == false)
            ++_waitTime[i];
            if(_waitTime[i] >= 60 && _isThrow == false)
            {
                _isJump[i] = true;
                _isThrow = true;
                _waitTime[i] = 0;
            }

            if(_isJump[i] == true)
            {
                ++_jumpCount[i];

                _characterView[i].transform.localPosition
                    = new Vector3(_characterView[i].transform.localPosition.x,
                                  _characterView[i].transform.localPosition.y
                                + _jumpPower - _gravity * _jumpCount[i] * _jumpCount[i],
                                  _characterView[i].transform.localPosition.z);


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

    }

    private void UpdateOfThrowBall()
    {
        if (_isThrow == true)
        {
            ++_throwCount;

            _throwBall.transform.localPosition
                = new Vector3(_throwBall.transform.localPosition.x + _throwSpeed.x,
                              _throwBall.transform.localPosition.y + _throwSpeed.y,
                              _throwBall.transform.localPosition.z);

            if (_throwCount % 20 == 0)
                _throwSpeed.y = _throwSpeed.y * -1.0f;

            if (_throwCount % 40 == 0)
            {
                _throwSpeed.x = _throwSpeed.x * -1.0f;
                _isThrow = false;
            }
        }
    }

}
