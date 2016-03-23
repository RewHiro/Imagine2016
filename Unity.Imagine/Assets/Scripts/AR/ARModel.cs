
using UnityEngine;

public class ARModel : MonoBehaviour {

  [SerializeField]
  [Tooltip("このモデルを表示するマーカー")]
  Texture2D _marker = null;

  [SerializeField]
  ActionManager _action = null;
  public ActionManager action { get { return _action; } }

  /// <summary> モデルを表示するマーカー </summary>
  public Texture2D marker { get { return _marker; } }

  /// <summary> マーカーに対応した ID </summary>
  public int id { get; private set; }

  public void MarkerSetup(ARDeviceManager device, int ir, int ie, int im) {
    id = device.arSystem.addARMarker(_marker, ir, ie, im);
  }
}
