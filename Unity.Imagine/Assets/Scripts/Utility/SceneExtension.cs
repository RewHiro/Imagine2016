
using UnityEngine.SceneManagement;

public enum GameScene {
  Title,
  Menu,
  Create,
  Printer,
  MiniGame,

  Max, None = -1,
}

public static class SceneExtension {

  public static bool ChangeScene(this GameScene scene) {
    return false;
  }
}
