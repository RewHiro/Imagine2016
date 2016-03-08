using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyGage : MonoBehaviour {
    
    [SerializeField]
    Gage _gage;

    [SerializeField]
    Image _iamge;

    [SerializeField]
    float _cross;

    Vector2 _size = new Vector2(0,0);

    Vector2 _gagePosition = new Vector2(0, 0);

    [SerializeField]
    float _speed;

	void Start ()
    {
        _size = _iamge.rectTransform.sizeDelta;
    }
	
	void Update ()
    {
	if(Input.GetKey(KeyCode.C))
        {
            Debug.Log(_gage._getChargeScore);
            PowerGage();
        }
	}

   public void PowerGage()
    {

        if (_gage._getChargeScore * _cross < _iamge.rectTransform.sizeDelta.x) return;
            _size.x += _speed;
            _iamge.rectTransform.sizeDelta = _size;
        _gagePosition = _iamge.rectTransform.anchoredPosition;
        _gagePosition.x += _speed / 4;
        _iamge.rectTransform.anchoredPosition = _gagePosition;
    }
}
