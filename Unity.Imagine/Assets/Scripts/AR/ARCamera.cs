
using UnityEngine;
using NyARUnityUtils;
using NyAR.MarkerSystem;

//------------------------------------------------------------
// TIPS:
// ゲーム空間を描画する、ゲームとしてのカメラ
//
//------------------------------------------------------------

public class ARCamera : MonoBehaviour {

  NyARUnityMarkerSystem _arSystem = null;
  public NyARUnityMarkerSystem arSystem { get { return _arSystem; } }

  [SerializeField]
  Camera _camera = null;
  
  void Start() {
    var manager = ARDeviceManager.instance;

    var config = new NyARMarkerSystemConfig(manager.device.width, manager.device.height);
    _arSystem = new NyARUnityMarkerSystem(config);
    _arSystem.setARBackgroundTransform(manager.cameraScreen.transform);
    _arSystem.setARCameraProjection(_camera);
  }

  public void UpdateView(NyARUnityWebCam device) { _arSystem.update(device); }
}
