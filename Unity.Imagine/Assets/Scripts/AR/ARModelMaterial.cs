
using UnityEngine;

public class ARModelMaterial : MonoBehaviour {

  [SerializeField]
  Material _player1 = null;
  public Material p1material { get { return _player1; } }

  [SerializeField]
  Material _player2 = null;
  public Material p2material { get { return _player2; } }
}
