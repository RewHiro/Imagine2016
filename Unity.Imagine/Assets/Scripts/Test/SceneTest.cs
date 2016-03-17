
using UnityEngine;

public class SceneTest : MonoBehaviour {

  [SerializeField]
  bool _activate = false;

  void Start() { GameScene.Title.ChangeScene(); }
}
