
using UnityEngine;

public class SceneTest : MonoBehaviour {

  [SerializeField]
  AudioPlayer[] _players = null;

  bool InputKey(KeyCode code) { return Input.GetKeyDown(code); }

  void Update() {
    if (InputKey(KeyCode.A)) { _players[0].Play(0); }
    if (InputKey(KeyCode.S)) { _players[1].Play(0); }
    if (InputKey(KeyCode.D)) { _players[2].Play(0); }
    if (InputKey(KeyCode.F)) { _players[3].Play(0); }
  }

  /*
  [SerializeField, Range(0, 1)]
  uint _soundIndex = 0;

  void Update() {
    if (!TouchController.IsTouchBegan()) { return; }
    AudioManager.instance.effect.Play(_soundIndex);
  }
  */
}
