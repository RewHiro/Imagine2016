
using UnityEngine;
using System;

class TouchTest : MonoBehaviour {
  void Update() {
    if (!TouchController.IsTouchBegan()) { return; }

    Debug.Log(">>>>> touch test");

    var hit = new RaycastHit();
    if (!TouchController.IsRaycastHit(out hit)) { return; }
    Debug.Log("hit: " + hit.transform.name + "/" + hit.transform);
  }

  void TransformScreenPosition() {
    var touch = TouchController.GetScreenPosition();
    Debug.Log("touch = " + touch);

    Action<Vector3> PrintLog = position => {
      var _stov = Camera.main.ScreenToViewportPoint(position);
      var _vtos = Camera.main.ViewportToScreenPoint(position);
      Debug.Log("stov = " + _stov);
      Debug.Log("vtos = " + _vtos);
    };

    var stov = Camera.main.ScreenToViewportPoint(touch);
    var vtos = Camera.main.ViewportToScreenPoint(touch);
    Debug.Log("stov = " + stov);
    Debug.Log("vtos = " + vtos);

    Debug.LogWarning("----- stov -----");
    PrintLog(stov);
    Debug.LogWarning("----- vtos -----");
    PrintLog(vtos);
  }
}
