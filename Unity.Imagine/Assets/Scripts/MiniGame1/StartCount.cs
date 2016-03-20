using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartCount : MonoBehaviour {

    [SerializeField]
    KeyAction _gameManager = null;

    [SerializeField]
    float _drawTime = 4.0f;

    float _time;

    float _regularInterval;

    [SerializeField, TooltipAttribute("表示する順番にImageを入れてください")]
    Image[] _startCountImage = null;

    bool _countFinish = false;
    public bool getCountFinish { get { return _countFinish; } }

    void Start ()
    {
        _regularInterval = _drawTime / _startCountImage.Length;
        _time = _drawTime;
        foreach (var image in _startCountImage)
        {
            image.enabled = false;
        }

        if (_gameManager == null) { Debug.Log("_gameManager が null です。KeyAction スクリプトが入ってるオブジェクトをいれてください。"); }
    }
	
	void Update ()
    {
        if (_gameManager == null || !_gameManager.isGameStart) { return; }
        CountDown();
        CountDrawImage();
    }

    void  CountDown()
    {
        if (_time <= 0) return;
        _time -= Time.deltaTime;
        
    }

    void CountDrawImage()
    {
        if(_time <= _drawTime && _time > _regularInterval * 3)
        {
            _startCountImage[0].enabled = true;
        }
        else
                if (_time <= _regularInterval * 3 && _time > _regularInterval * 2)
        {
            _startCountImage[0].enabled = false;
            _startCountImage[1].enabled = true;
        }
        else
        if (_time <= _regularInterval * 2 && _time > _regularInterval)
        {
            _startCountImage[1].enabled = false;
            _startCountImage[2].enabled = true;
        }
        else
        if ( _time < _regularInterval && _time > 0)
        {
            _startCountImage[2].enabled = false;
            _startCountImage[3].enabled = true;
        }
        else
        if (_time <= 0)
        {
            _startCountImage[3].enabled = false;
            _countFinish = true;
        }

    }
}
