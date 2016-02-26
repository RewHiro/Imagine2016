
using UnityEngine;
using System.Collections;

public class ARModel : MonoBehaviour {

  public int id { get; private set; }

  [SerializeField]
  Texture2D _marker = null;

  [SerializeField]
  [Range(10f, 100f)]
  float _scale = 50f;

  void Start() { if (_marker != null) { StartCoroutine(UpdateModel()); } }

  IEnumerator UpdateModel() {
    var ar = ARCamera.instance;
    id = ar.arSystem.addARMarker(_marker, 16, 25, 80);
    ar.models.Add(this);
    transform.localScale = Vector3.one * _scale;

    while (true) {
      if (ar.arSystem.isExistMarker(id)) {
        transform.position = Vector3.zero;
        ar.arSystem.setMarkerTransform(id, transform);
        transform.Rotate(Vector3.right * 90f);
      }
      yield return null;
    }
  }
}
