using UnityEngine;
using UnityEngine.UI;

public class Gage : MonoBehaviour
{

    [SerializeField]
    private Image _image;

    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private Image[] _rangeGageImage = null;

    private Vector2 _size;

    private Image _selectGage;

    private Vector3 _selectGagePosition = new Vector3(0, 0, 0);

    private int _score = 0;

    public int _getChargeScore { get { return _score; } }

    void Start()
    {
        _selectGage = GetComponent<Image>();
        _size = _image.rectTransform.sizeDelta;
        _selectGagePosition = _image.rectTransform.anchoredPosition;
        _selectGagePosition.x -= _size.x / 2;
        _selectGage.rectTransform.anchoredPosition = _selectGagePosition;
    }

    public void MoveSelectGage()
    {
        if (_selectGage.rectTransform.anchoredPosition.x >=
            _image.rectTransform.anchoredPosition.x + _size.x / 2) return;
        _selectGagePosition.x += _speed;
        _selectGage.rectTransform.anchoredPosition = _selectGagePosition;
        _score = RangeSelectNow();
    }

    int RangeSelectNow()
    {
        int score = 0;
        if (_rangeGageImage.Length == 0) return 0;

        foreach (Image selectRange in _rangeGageImage)
        {
            if (_selectGage.rectTransform.anchoredPosition.x >=
                selectRange.rectTransform.anchoredPosition.x -
                selectRange.rectTransform.sizeDelta.x / 2 &&
                _selectGage.rectTransform.anchoredPosition.x <=
                selectRange.rectTransform.anchoredPosition.x +
                selectRange.rectTransform.sizeDelta.x / 2)
            {
                score++;
            }
        }

        return score;

    }

    public void InitGage()
    {
        _selectGagePosition = _image.rectTransform.anchoredPosition;
        _selectGagePosition.x -= _size.x / 2;
        _selectGage.rectTransform.anchoredPosition = _selectGagePosition;

    }

}
