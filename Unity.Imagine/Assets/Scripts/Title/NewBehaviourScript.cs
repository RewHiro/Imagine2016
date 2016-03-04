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
    private int[] _totalWaitTime = new int[2];
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


    //こける処理
    //何回目
    private int _kickCount = 0;
    private int _totalKickCount = 5;
    private int _characterID = 0;

    private int[] _fallSpeed = new int[2];
    private bool[] _isFall = new bool[2];
    private int[] _fallCount = new int[2];
    private int[] _totalFallCount = new int[2];

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
            _isFall[i] = false;
            _fallSpeed[0] = -6;
            _fallSpeed[1] = 6;
            _fallCount[i] = 0;
            _totalFallCount[i] = 0;
            _totalWaitTime[i] = UnityEngine.Random.Range(60, 90);
        }
	}
	
	void Update ()
    {
        UpdateOfNameLogo();
        UpdateOfClouds();
        UpdateOfCharacterView();
        UpdateOfCharacterFall();
      //  UpdateOfThrowBall();
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
            if(_waitTime[i] >= _totalWaitTime[i] && _isThrow == false)
            {
                _totalWaitTime[i] = UnityEngine.Random.Range(60,90);
                _isJump[i] = true;
                ++_kickCount;
                if(_kickCount % _totalKickCount == 0)
                    _isFall[i] = true;

                //_isThrow = true;

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
                
                if(_isFall[i] == false)
                {
                    _characterView[i].transform.Rotate(new Vector3(0, 24, 0));
                }


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

    private void UpdateOfCharacterFall()
    {
        for (int i = 0; i < _characterView.Length; ++i)
        {
            if (_isFall[i] == true)
            {
                ++_fallCount[i];
                _characterView[i].transform.Rotate(new Vector3(0,0,_fallSpeed[i]));
                if (_fallCount[i] == 15)
                {
                    _fallSpeed[i] *= -1;
                    _isJump[i] = true;
                }

                if (_fallCount[i] >= 30)
                {
                    _isFall[i] = false;
                    //_fallSpeed[i] *= -1;
                    _fallCount[i] = 0;
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
