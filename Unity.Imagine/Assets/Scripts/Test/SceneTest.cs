
using UnityEngine;
using System.Collections;
using System.Linq;

public class SceneTest : MonoBehaviour {

  void Start() { StartCoroutine(Test()); }

  IEnumerator Test() {
    var audio = AudioManager.instance;
    var source = audio.CreateSource();

    System.Action Log = () => Debug.Log("sources count = " + source.GetSources().Count());

    Log();
    for (var i = 0; i < 10; ++i) { source.AddSource(); }

    while (source.GetSources().Count() > 0) {
      Log();
      source.Refresh();
      yield return null;
    }

    Log();
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
