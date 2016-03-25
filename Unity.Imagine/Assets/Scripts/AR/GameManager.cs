
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

  [SerializeField]
  ARDeviceManager _device = null;

  [SerializeField]
  GameController _controller = null;

  bool _isStart = false;
  public bool isStart { get { return _isStart; } }

  Coroutine _playThread = null;

  void Start() {
    _playThread = StartCoroutine(DetectMarker());
  }

  /// <summary> プレイボタンが押された </summary>
  public void OnPlay() {
    StopCoroutine(_playThread);
    _playThread = StartCoroutine(GameLoop());
  }

  /// <summary> 戻るボタンが押された </summary>
  public void OnBackToMenu() {
    StopCoroutine(_playThread);
    _playThread = StartCoroutine(DetectMarker());
  }

  IEnumerator GameLoop() {
    yield return StartCoroutine(Standby());
    yield return StartCoroutine(MainGame());
    yield return StartCoroutine(Result());
  }

  // TIPS: マーカー検出
  IEnumerator DetectMarker() {
    _isStart = false;

    while (!_isStart) {
      _isStart = _device.DetectMarker();
      yield return null;
    }

    _device.player1.inputKey = _controller.IsPlayer1KeyDown;
    _device.player2.inputKey = _controller.IsPlayer2KeyDown;
    _device.player1.transform.LookAt(_device.player2.transform);
    _device.player2.transform.LookAt(_device.player1.transform);
  }

  // TIPS: ゲームルール説明（キー入力でゲーム開始）
  IEnumerator Standby() {
    while (!_isStart) {
      _isStart = _controller.IsPlayer1KeyDown() && _controller.IsPlayer2KeyDown();
      _device.ModelUpdate();
      yield return null;
    }
    Debug.Log("Finish");
  }

  // TIPS: ゲームループ
  IEnumerator MainGame() {
    yield return null;
  }

  // TIPS: リザルト表示
  IEnumerator Result() {
    yield return null;
  }
}
