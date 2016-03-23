
using UnityEngine;
using System.Collections.Generic;
using NyAR.MarkerSystem;
using NyARUnityUtils;

//------------------------------------------------------------
// TIPS:
// 接続されているデバイスとしてのカメラを管理する
// ARCamera を登録、まとめて更新する
//
//------------------------------------------------------------

public class ARDeviceManager : MonoBehaviour {

  NyARUnityWebCam _device = null;
  public NyARUnityWebCam device { get { return _device; } }

  NyARUnityMarkerSystem _arSystem = null;
  public NyARUnityMarkerSystem arSystem { get { return _arSystem; } }

  [SerializeField]
  Camera _camera = null;

  [SerializeField]
  [Tooltip("カメラ映像を投影するパネルの Renderer コンポーネント")]
  Renderer _panel = null;

  /// <summary> カメラ映像を投影しているパネルオブジェクト </summary>
  public GameObject cameraScreen { get { return _panel.gameObject; } }

  [SerializeField]
  Vector3 _scale = Vector3.zero;

  [SerializeField, Range(2, 64)]
  [Tooltip("マーカーの解像度")]
  int _resolution = 16;

  [SerializeField, Range(10, 50)]
  [Tooltip("マーカーのエッジ割合")]
  int _edge = 25;

  [SerializeField, Range(10, 320)]
  [Tooltip("マーカーサイズ")]
  int _markerScale = 80;

  /// <summary> 管理下にある <see cref="ARModel"/> を全て取得 </summary>
  public IEnumerable<ARModel> models { get { return this.GetOnlyChildren<ARModel>(); } }

  void Awake() {
    if (WebCamTexture.devices.Length <= 0) { return; }

    var wcTexture = new WebCamTexture(320, 240, 15);
    _device = NyARUnityWebCam.CreateInstance(wcTexture);
    _panel.material.mainTexture = wcTexture;

    var config = new NyARMarkerSystemConfig(_device.width, _device.height);
    _arSystem = new NyARUnityMarkerSystem(config);
    _arSystem.setARBackgroundTransform(_panel.transform);
    _arSystem.setARCameraProjection(_camera);
  }

  void Start() {
    _device.Start();
    foreach (var model in models) {
      model.transform.localScale = _scale;
      model.MarkerSetup(this, _resolution, _edge, _markerScale);
    }
  }

  void FixedUpdate() {
    _device.Update();
    _arSystem.update(_device);

    foreach (var model in models) {
      if (!_arSystem.isExistMarker(model.id)) { continue; }

      model.transform.position = Vector3.zero;
      arSystem.setMarkerTransform(model.id, model.transform);
      model.action.Rotate();
    }
  }

  public void OnDestroy() { _device.Stop(); }
}
