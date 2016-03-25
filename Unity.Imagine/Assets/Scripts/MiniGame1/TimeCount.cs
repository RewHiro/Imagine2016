
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {

  [SerializeField]
  Text _board = null;
  
  [SerializeField]
  ScoreCompare _scoreCompare = null;

  [SerializeField, Range(5f, 15f)]
  float _timeCount = 10;
  public float timeCount { get { return _timeCount; } }

  public float time { get; set; }

  void Start() { time = _timeCount; }

  /// <summary> 残り時間を減らす </summary>
  public void UpdateTimeCount() { if (time > 0f) time -= Time.deltaTime; }

  void Update() {
    if (_scoreCompare.getDisplayScore) { return; }
    _board.text = "Time: " + Mathf.RoundToInt(time).ToString();
  }
}
