using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyGage : MonoBehaviour {
    
    [SerializeField]
    Gage _gage;

    [SerializeField]
    Image _powerIamge;

    [SerializeField]
    Image _backgroundGage;

    [SerializeField]
    float _cross;

    [SerializeField]
    int _player2Gage = 1 ;

    Vector2 _size = new Vector2(0,0);

    Vector2 _gagePosition = new Vector2(0, 0);

    [SerializeField]
    float _speed = 0;

    bool _isPowerGage = false;

    public bool _getIsPowerGage {get { return _isPowerGage; }}

    void Start ()
    {
        //_gagePosition = _backgroundGage.rectTransform.anchoredPosition;
        //_gagePosition.x = (_backgroundGage.rectTransform.anchoredPosition.x + (_backgroundGage.rectTransform.sizeDelta.x / 4));
        //_powerIamge.rectTransform.anchoredPosition = _gagePosition;
        _size = _powerIamge.rectTransform.sizeDelta;
        //Debug.Log(_powerIamge.rectTransform.anchoredPosition );
    }
	 
	void Update (){}


    public bool PowerGage()
    {
        //ちょっとゲージがずれる(後で直す)
        if (_gage._getChargeScore * _cross > _powerIamge.rectTransform.sizeDelta.x)
        {
            _size.x += _speed;
            _powerIamge.rectTransform.sizeDelta = _size;
            _gagePosition = _powerIamge.rectTransform.anchoredPosition;
            _gagePosition.x += _speed / 4 * _player2Gage;
            _powerIamge.rectTransform.anchoredPosition = _gagePosition;
            return _isPowerGage = false;
        }
        else

        if (_gage._getChargeScore * _cross <=
            _powerIamge.rectTransform.sizeDelta.x)
        {
            if (true)
            {return _isPowerGage = false;}
            else
            if (false)
            {
                return _isPowerGage = true;
            }
        }

        return _isPowerGage = false;
    }
}
