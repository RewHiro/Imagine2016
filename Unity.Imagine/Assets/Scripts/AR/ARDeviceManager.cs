
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NyAR.MarkerSystem;
using NyARUnityUtils;

//------------------------------------------------------------
// TIPS:
// 接続されているデバイスとしてのカメラを管理する
// ARCamera を登録、まとめて更新する
//
//------------------------------------------------------------

public class ARDeviceManager : SingletonBehaviour<ARDeviceManager> {

  NyARUnityWebCam _device = null;
  public NyARUnityWebCam device { get { return _device; } }

  NyARUnityMarkerSystem _arSystem = null;
  public NyARUnityMarkerSystem arSystem { get { return _arSystem; } }

  struct Marker {
    public Marker(int newID) {
      id = newID;
      position = Vector3.zero;
      rotation = Quaternion.identity;
    }
    public int id;
    public Vector3 position;
    public Quaternion rotation;
  }

  [SerializeField]
  Camera _camera = null;

  [SerializeField]
  [Tooltip("カメラ映像を投影するパネル")]
  GameObject _panel = null;

  /// <summary> カメラ映像を投影しているパネルオブジェクト </summary>
  public GameObject cameraScreen { get { return _panel; } }

  bool _signal = false;

  protected override void Awake() {
    base.Awake();
    if (WebCamTexture.devices.Length <= 0) { return; }

    var wcTexture = new WebCamTexture(320, 240, 15);
    _device = NyARUnityWebCam.CreateInstance(wcTexture);
    _panel.GetComponent<Renderer>().material.mainTexture = wcTexture;

    var config = new NyARMarkerSystemConfig(_device.width, _device.height);
    _arSystem = new NyARUnityMarkerSystem(config);
    _arSystem.setARBackgroundTransform(_panel.transform);
    _arSystem.setARCameraProjection(_camera);
  }

  void Start() { StartDevice(); }

  /// <summary> デバイスを停止する </summary>
  public void StopDevice() { _signal = false; }

  /// <summary> AR カメラを起動する </summary>
  public void StartDevice() { StartCoroutine(UpdateDevice()); }

  IEnumerator UpdateDevice() {
    _device.Start();
    _signal = true;

    var modelManager = ARModelManager.instance;
    var markerList = new List<Marker>();

    System.Action ResetList = () => {
      if (markerList.Count == modelManager.models.Count) { return; }
      markerList.Clear();
      foreach (var model in modelManager.models) { markerList.Add(new Marker(model.id)); }
    };

    while (_signal) {
      _device.Update();
      _arSystem.update(_device);
      ResetList();
      foreach (var model in modelManager.models) {

        Debug.Log(model.id);
        var marker = markerList.Find(mark => mark.id == model.id);
        _arSystem.getMarkerTransform(model.id, ref marker.position, ref marker.rotation);
        Debug.Log(marker.position);
        Debug.Log(marker.rotation);

        if (!_arSystem.isExistMarker(model.id)) { continue; }
        transform.position = Vector3.zero;
        arSystem.setMarkerTransform(model.id, model.transform);
        //transform.Rotate(Vector3.right * 90f);
        model.gameObject.transform.eulerAngles = model.gameObject.GetComponent<ActionManager>().rotation;
      }

      Debug.Log(markerList.Count);

      yield return null;
    }

    _device.Stop();
  }
}
