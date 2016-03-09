using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour {

    public KeyCode keyCode { get; set; }

    public GameObject Enemy { get; set; }

    public virtual void Action() { /*Debug.Log("仮想メソッドです。継承して使ってください。");*/ }
}
