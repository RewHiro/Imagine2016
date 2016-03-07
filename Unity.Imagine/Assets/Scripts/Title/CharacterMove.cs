using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    GameObject _character = null;

    [SerializeField]
    bool _canJump;

    [SerializeField]
    bool _canSpin;

    //何回目でこかすか
    [SerializeField]
    int _totalKickCount;
    //飛ぶ威力
    [SerializeField]
    int _jumpPower;

    //重力
    [SerializeField]
    float _gravity;

    //待っている時間
    [SerializeField]
    int _waitTime;

    //こけるSpeed
    [SerializeField]
    int _spinSpeed;

    public struct CharacterStatus
    {

        //最大待ち時間
        public int _totalWaitTime;
        //飛んでいるかどうか
        public bool _isJump;
        //ジャンプしている時間
        public int _jumpCount;
        //_ジャンプ開始位置
        public float _tempPosY;
        //こけているかどうか
        public bool _isSpin;
        //こけている秒数
        public int _fallCount;
        //こけている最大時間
        public int _totalFallCount;
    }

    //こける処理
    //1回飛ぶごとにCountを＋１
    private int _kickCount = 0;

    CharacterStatus _characterStatus;

    void Start()
    {
        _characterStatus._totalWaitTime = 0;
        _characterStatus._isJump = false;
        _characterStatus._jumpCount = 0;
        _characterStatus._tempPosY = _character.transform.localPosition.y;
        _characterStatus._isSpin = false;
        _characterStatus._fallCount = 0;
        _characterStatus._totalFallCount = 30;
    }


    void Update()
    {
        UpdateOfCharacterSetIsJump();
        UpdateofCharacterJump();
        UpdateOfCharacterSpin();
    }

    private void UpdateOfCharacterSetIsJump()
    {
        if (_characterStatus._isJump == false && _canJump == true)
            ++_waitTime;

        if (_waitTime >= _characterStatus._totalWaitTime)
        {
            //Randomで飛ぶタイミングを変更
            _characterStatus._totalWaitTime = UnityEngine.Random.Range(60, 90);
            _characterStatus._isJump = true;
            ++_kickCount;
            //Trueならこかす
            if (_kickCount % _totalKickCount == 0 && _canSpin == true)
                _characterStatus._isSpin = true;

            _waitTime = 0;
        }
    }

    private void UpdateofCharacterJump()
    {
        if (_characterStatus._isJump == true)
        {
            ++_characterStatus._jumpCount;

            _character.transform.localPosition
                = new Vector3(_character.transform.localPosition.x,
                              _character.transform.localPosition.y
                            + _jumpPower - _gravity * _characterStatus._jumpCount * _characterStatus._jumpCount,
                              _character.transform.localPosition.z);

            //こけていないときに回転させる
            if (_characterStatus._isSpin == false)
            {
                _character.transform.Rotate(new Vector3(0,_spinSpeed * 4, 0));
            }

            //初期位置より下にいったら元の位置に戻し、ジャンプ処理の終了
            if (_character.transform.localPosition.y < _characterStatus._tempPosY)
            {
                _characterStatus._isJump = false;
                _character.transform.localPosition
                = new Vector3(_character.transform.localPosition.x,
                              _characterStatus._tempPosY,
                              _character.transform.localPosition.z);
                _characterStatus._jumpCount = 0;
            }
        }
    }

    private void UpdateOfCharacterSpin()
    {
        if (_characterStatus._isSpin == true)
        {
            ++_characterStatus._fallCount;
            //Rotateをいじりこかす
            _character.transform.Rotate(new Vector3(0, 0,_spinSpeed));
            //Count 90 / fallSpeed = 15なので　15基準です
            if (_characterStatus._fallCount == 15)
            {
                //起こすためにSpeedを-1
                _spinSpeed *= -1;
                _characterStatus._isJump = true;
            }

            //起きたら戻す
            if (_characterStatus._fallCount >= 30)
            {
                _characterStatus._isSpin = false;
                _characterStatus._fallCount = 0;
            }

        }
    }
}
