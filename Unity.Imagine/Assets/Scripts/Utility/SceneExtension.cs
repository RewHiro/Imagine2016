
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameScene {
  Title,
  Menu,
  Create,
  Printer,
  MiniGames,

  Max, None = -1,
}

public static class SceneExtension {

  /// <summary> シーンを切り替える </summary>
  public static void ChangeScene(this GameScene scene) {
    SceneManager.LoadScene(scene.ToString());
  }

  /// <summary> 実行中のシーンを全て取得 </summary>
  public static IEnumerable<Scene> GetAllActiveScenes() {
    var count = SceneManager.sceneCount;
    for (var i = 0; i < count; ++i) { yield return SceneManager.GetSceneAt(i); }
  }
}
