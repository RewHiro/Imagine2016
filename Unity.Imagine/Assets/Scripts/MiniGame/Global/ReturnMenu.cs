using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReturnMenu : MonoBehaviour
{

    [SerializeField]
    float _delayTime = 2.0f;

    [SerializeField]
    AudioPlayer _audioPlayer;


    [SerializeField]
    private LayerMask _mask;

    [SerializeField]
    private TimeCount _timeCount = null;

    [SerializeField]
    Text _text = null;

    [SerializeField]
    float _turnAngle = 360.0f;
    [SerializeField]
    float _turnSpeed = 20.0f;

    bool _isTurn = false;
    bool _isReturn = false;
    bool _isWinner = false;
    bool _isRotationEnd = false;
    bool _isReturnNenu = false;
    float time;
    public bool getIsRotationEnd { get { return _isRotationEnd; } }

    ScoreCompare _scoreCompare;

    void Start()
    {
        time = _delayTime;
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _scoreCompare = FindObjectOfType<ScoreCompare>();
        _audioPlayer.Play(3, 0.2f, true);
    }

    // Update is called once per frame
    void Update()
    {
        //Turn();
        Turn();
        ReturnMenuUpdate();
        WaitTime();
        // この１文はタイムが０になったら起こるようにしてあるので
        // 必要なかったり、別のところで必要になったら消してください。
        if (_scoreCompare.getDisplayScore == true) { WinnerPlayer(); }


        
    }

    void Turn()
    {
        if (_isTurn)
        {

            _turnAngle -= _turnSpeed;
            if (_turnAngle <= 0.0f)
            {
                _isTurn = false;
                //isReturn = true;
                //直す
                _isRotationEnd = true;
                _turnAngle = 720.0f;
                _audioPlayer.Play(19, false);
                _isReturnNenu = true;
            }
            transform.eulerAngles = new Vector3(0.0f, _turnAngle, 0.0f);
        }
    }

    void WaitTime()
    {
        if (time <= 0 || _isReturnNenu == false) return;
        
        time -= Time.deltaTime;
        if(time <= 0)
        {
            ReturnOn();
        }
    }

    void WinnerPlayer()
    {
        if (_isWinner == true) return;

        if (_scoreCompare.getWinPlayer == ScoreCompare.WinPlayer.Player1)
        {
            _isWinner = true;
            _isTurn = true;
            _text.text = "←";
        }
        else
        if (_scoreCompare.getWinPlayer == ScoreCompare.WinPlayer.Player2)
        {
            _isWinner = true;
            _isTurn = true;
            _text.text = "→";
        }

    }

    void ReturnMenuUpdate()
    {
        if (!_isReturn) { return; }
        if (_timeCount._getTime > 0) { return; }

        if (_timeCount != null) { Destroy(_timeCount); }

        _text.text = "メニューに戻る";

        if (Input.GetMouseButtonDown(0))
        {

            //Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;
            if (TouchController.IsRaycastHitWithLayer(out hit, _mask))
            {
                if (hit.transform.name == transform.name)
                {
                    ScreenSequencer.instance.SequenceStart(() => GameScene.Menu.ChangeScene(), new Fade(1f));
                }
            }
        }
    }

    // これを呼べばメニューに戻る
    public void ReturnOn()
    {
        _isTurn = true;
        _isReturn = true;
    }
}
