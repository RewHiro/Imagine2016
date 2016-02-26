
using UnityEngine;
using Game.Utility;

public class PrintTest : MonoBehaviour {

  void Start() {
    var path = Application.dataPath + "/Resources/test.png";
    PrintDevice.PrintRequest(path);
  }
}
