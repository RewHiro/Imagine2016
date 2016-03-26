
using UnityEngine;

public class GameShot : MonoBehaviour {

  [SerializeField]
  MeshRenderer _renderer = null;
  public new MeshRenderer renderer { get { return _renderer; } }

  [SerializeField, Range(1f, 20f)]
  float _velocity = 5f;

  public Transform target { get; set; }
  Vector3 distance { get { return target.position - transform.position; } }

  // TIPS: 一度発射されたらターゲットに向かって進み続ける
  void FixedUpdate() {
    var velocity = distance.normalized * _velocity;
    transform.position += velocity;
  }
}
