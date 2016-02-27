
using UnityEngine;

class TouchTest : MonoBehaviour {
  void Update() {
    if (!TouchController.IsTouchBegan()) { return; }

    Debug.Log(">>>>> touch test");

    var touch = TouchController.GetScreenPosition();
    Debug.Log("touch = " + touch);

    var stov = Camera.main.ScreenToViewportPoint(touch);
    var vtos = Camera.main.ViewportToScreenPoint(touch);
    Debug.Log("stov = " + stov);
    Debug.Log("vtos = " + vtos);

    Debug.LogWarning("----- stov -----");
    PrintLog(stov);
    Debug.LogWarning("----- vtos -----");
    PrintLog(vtos);
  }

  void PrintLog(Vector3 position) {
    var stov = Camera.main.ScreenToViewportPoint(position);
    var vtos = Camera.main.ViewportToScreenPoint(position);
    Debug.Log("stov = " + stov);
    Debug.Log("vtos = " + vtos);
  }
}
