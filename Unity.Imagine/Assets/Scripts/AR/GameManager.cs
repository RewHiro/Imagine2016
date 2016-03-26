
using UnityEngine;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour {

  [SerializeField]
  ARDeviceManager _device = null;

  [SerializeField]
  GameController _controller = null;

  [SerializeField]
  GameMenu _menu = null;

  [SerializeField]
  CanvasGroup _gameUI = null;

  [SerializeField]
  TimeCount _counter = null;

  [SerializeField]
  GameCounter _startCount = null;

  [SerializeField]
  GameCounter _finishCount = null;

  [SerializeField]
  GameSuddenDeath _suddenDeath = null;

  [SerializeField]
  GameFinish _finish = null;

  [SerializeField]
  ARModelMaterial _materials = null;

  [SerializeField]
  GameObject _shot = null;

  [SerializeField]
  GameObject _ruleCanvas = null;

  bool _isStart = false;

  public enum State { Detect, Standby, MainGame, Result, }
  State _state = State.Detect;
  public State state { get { return _state; } }

  /// <summary> プレイボタンが押せるようになったら true を返す </summary>
  public bool isStart { get { return _isStart; } }

  Coroutine _playThread = null;

  void Start() {
    _playThread = StartCoroutine(GameLoop());
    _gameUI.alpha = 0f;
  }

  /// <summary> プレイボタンが押された </summary>
  public void OnPlay() { _isStart = true; }

  /// <summary> 戻るボタンが押された </summary>
  public void OnBackToMenu() {
    StopCoroutine(_playThread);
    System.Action ChangeScene = () => { GameScene.Menu.ChangeScene(); };
    ScreenSequencer.instance.SequenceStart(ChangeScene, new Fade(1f));
  }

  IEnumerator GameLoop() {
    yield return StartCoroutine(DetectMarker());
    yield return StartCoroutine(Standby());
    yield return StartCoroutine(MainGame());
    yield return StartCoroutine(Result());
  }

  // TIPS: マーカー検出
  IEnumerator DetectMarker() {
    _state = State.Detect;
    _isStart = false;

    while (!_isStart) {
      _menu.start.interactable = _device.DetectMarker();
      yield return null;
    }

    _device.player1.inputKey = _controller.IsPlayer1KeyDown;
    _device.player2.inputKey = _controller.IsPlayer2KeyDown;
    _device.player1.renderer.material = _materials.p1material;
    _device.player2.renderer.material = _materials.p2material;
  }

  // TIPS: ゲームルール説明（キー入力でゲーム開始）
  IEnumerator Standby() {
    _state = State.Standby;

    //ゲームルールのキャンバス表示
    //var canvas = Instantiate(_ruleCanvas);

    while (!_isStart) {
      _isStart = _controller.IsPlayer1KeyDown() && _controller.IsPlayer2KeyDown();
      _device.ModelUpdate();
      yield return null;
    }

    //キャンバス削除
    //Destroy(canvas);

    while (_menu.group.alpha > 0f) {
      var time = Time.deltaTime;
      _menu.group.alpha -= time;
      _gameUI.alpha += time;
      yield return null;
    }
  }

  // TIPS: ゲームループ
  IEnumerator MainGame() {
    _state = State.MainGame;

    while (_counter.time > 0f) {
      _device.ModelUpdate();

      // TIPS: モデルが表示されている間だけ実行
      if (_device.models.All(model => model.isVisible)) {
        _counter.UpdateTimeCount();
      }

      yield return null;
    }
  }

  // TIPS: リザルト表示
  IEnumerator Result() {
    _state = State.Result;

    while (true) {
      yield return null;
      break;//test
    }
    Debug.Log("result test");
  }
}
