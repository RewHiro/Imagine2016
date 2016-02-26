
using UnityEngine;
using System.Collections.Generic;
using NyARUnityUtils;
using jp.nyatla.nyartoolkit.cs.markersystem;

public class ARCamera : SingletonBehaviour<ARCamera> {

  NyARUnityMarkerSystem _arSystem = null;
  public NyARUnityMarkerSystem arSystem { get { return _arSystem; } }

  NyARUnityWebCam _arCamera = null;
  public NyARUnityWebCam arCamera { get { return _arCamera; } }

  List<ARModel> _models = null;
  public List<ARModel> models { get { return _models; } }

  [SerializeField]
  GameObject _panel = null;

  protected override void Awake() {
    base.Awake();
    if (WebCamTexture.devices.Length <= 0) { return; }

    var wcTexture = new WebCamTexture(320, 240, 15);
    _arCamera = NyARUnityWebCam.createInstance(wcTexture);

    var config = new NyARMarkerSystemConfig(_arCamera.width, _arCamera.height);
    _arSystem = new NyARUnityMarkerSystem(config);

    _panel.GetComponent<Renderer>().material.mainTexture = wcTexture;
    _arSystem.setARBackgroundTransform(_panel.transform);
    _arSystem.setARCameraProjection(Camera.main);

    _models = new List<ARModel>();
  }

  void Start() {
    _arCamera.start();
  }

  void Update() {
    _arCamera.update();
    _arSystem.update(_arCamera);
  }
}
