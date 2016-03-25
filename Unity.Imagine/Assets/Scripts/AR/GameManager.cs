
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

  enum State {
    DetectMarker,
    GameHint,
    GameLoop,
    Result,
  }
  State _state = State.DetectMarker;

  [SerializeField]
  ARDeviceManager _device = null;

  [SerializeField]
  GameController _controller = null;

  bool _isStart = false;
  public bool isStart { get { return _isStart; } }

  Coroutine _playThread = null;

  //DEBUG
  void Start() { StartCoroutine(GameLoop()); }

  /// <summary> プレイボタンが押された </summary>
  public void OnPlay() { _playThread = StartCoroutine(GameLoop()); }

  /// <summary> 戻るボタンが押された </summary>
  public void OnBackToMenu() { StopCoroutine(_playThread); }

  IEnumerator GameLoop() {
    yield return StartCoroutine(DetectMarker());
    yield return StartCoroutine(Standby());
    yield return StartCoroutine(MainGame());
    yield return StartCoroutine(Result());
  }

  IEnumerator DetectMarker() {
    while (!_device.DetectMarker()) { yield return null; }
    _device.player1.inputKey = _controller.IsPlayer1KeyDown;
    _device.player2.inputKey = _controller.IsPlayer2KeyDown;
  }

  IEnumerator Standby() {
    while (!_isStart) {
      _isStart = _controller.IsPlayer1KeyDown() && _controller.IsPlayer2KeyDown();
      _device.ModelUpdate();
      yield return null;
    }
    Debug.Log("Finish");
  }

  IEnumerator MainGame() {
    yield return null;
  }

  IEnumerator Result() {
    yield return null;
  }
}
