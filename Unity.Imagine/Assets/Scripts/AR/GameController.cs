
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour {

  [SerializeField]
  KeyCode[] _player1 = { KeyCode.S, };

  [SerializeField]
  KeyCode[] _player2 = { KeyCode.K, };

  void Awake() {
    System.Action<KeyCode[], KeyCode> Init = (keys, key) => {
      if (keys != null) { return; }
      keys = new KeyCode[] { key, };
    };
    Init(_player1, KeyCode.S);
    Init(_player2, KeyCode.K);
  }

  /// <summary> Player1 に割り当てられたキーが入力されたら true を返す </summary>
  public bool IsPlayer1KeyDown() {
    return _player1.Any(key => Input.GetKeyDown(key));
  }

  /// <summary> Player2 に割り当てられたキーが入力されたら true を返す </summary>
  public bool IsPlayer2KeyDown() {
    return _player2.Any(key => Input.GetKeyDown(key));
  }
}
