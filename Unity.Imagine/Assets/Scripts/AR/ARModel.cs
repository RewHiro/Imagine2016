
using UnityEngine;

public class ARModel : MonoBehaviour {

  [SerializeField]
  [Tooltip("このモデルを表示するマーカー")]
  Texture2D _marker = null;

  [SerializeField]
  [Tooltip("起動時に、指定された値でモデルのサイズを初期化する")]
  [Range(1f, 100f)]
  float _modelScale = 50f;

  /// <summary> モデルを表示するマーカー </summary>
  public Texture2D marker { get { return _marker; } }

  /// <summary> マーカーに対応した ID </summary>
  public int id { get; private set; }

  void Start() {
    id = ARDeviceManager.instance.arSystem.addARMarker(_marker, 8, 25, 160);
    transform.localScale = Vector3.one * _modelScale;

    ARModelManager.instance.models.Add(this);
  }
}
