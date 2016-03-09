using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DevelopmentViewChange : MonoBehaviour {

    [SerializeField, Range(0, 2), Tooltip("画像番号")]
    private int _index = 0;

    private Image _image = null;
    private List<Sprite> _sprite;

    void Start()
    {
        _sprite = new List<Sprite>();
        _sprite.Add(Resources.Load<Sprite>("DevelopmentView/型紙仮提出　タンク"));
        _sprite.Add(Resources.Load<Sprite>("DevelopmentView/型紙仮提出 ドウブツ"));
        _sprite.Add(Resources.Load<Sprite>("DevelopmentView/型紙仮提出　ヒト"));
        _image = GetComponent<Image>();
        _image.sprite = _sprite[_index];
    }

    void Update()
    {
        _image.sprite = _sprite[_index];
    }
}
