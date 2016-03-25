
using UnityEngine;

public class ARModel : MonoBehaviour {

  [SerializeField]
  [Tooltip("このモデルを表示するマーカー")]
  Texture2D _marker = null;

  [SerializeField]
  MeshRenderer _renderer = null;
  public new MeshRenderer renderer { get { return _renderer; } }

  [SerializeField]
  ActionManager _action = null;
  public ActionManager action { get { return _action; } }

  /// <summary> モデルを表示するマーカー </summary>
  public Texture2D marker { get { return _marker; } }

  /// <summary> マーカーに対応した ID </summary>
  public int id { get; private set; }

  /// <summary> マーカーを登録、ID を生成する </summary>
  public void MarkerSetup(ARDeviceManager device) {
    id = device.arSystem.addARMarker(_marker,
                                     device.markerResolution,
                                     device.markerEdge,
                                     device.markerScale);
  }

  /// <summary> 対象オブジェクトの方向に、正面を向ける </summary>
  public void LookAt(Transform target) {
    var distance = target.position - transform.position;
    transform.rotation.SetLookRotation(distance);
  }

  public bool isVisible { get; set; }

  public System.Func<bool> inputKey { get; set; }
}
