
using UnityEngine;
using UnityEngine.UI;

public class GameCounter : MonoBehaviour {

  [SerializeField]
  AudioPlayer _audio = null;

  [SerializeField, Range(14, 15)]
  int _clipIndex = 14;

  [SerializeField]
  bool _start = false;

  [SerializeField]
  Image _image = null;

  [SerializeField]
  Sprite[] _sprites = null;

  void Start() { _image.enabled = false; }

  /// <summary> 隠したり、表示したり </summary>
  public void Visible() { _image.enabled = !_image.enabled; }

  int _currentIndex = 3;

  /// <summary> カウンタ更新 </summary>
  public void UpdateCount(int count) {
    var index = Mathf.Clamp(count, 0, _sprites.Length - 1);
    var zero = (index == 0);
    if (index != _currentIndex && !zero) { _audio.Play(_clipIndex); }
    if (index != _currentIndex && zero) { _audio.Play(_start ? 13 : 16); }
    _currentIndex = index;
    _image.sprite = _sprites[_currentIndex];
  }
}
