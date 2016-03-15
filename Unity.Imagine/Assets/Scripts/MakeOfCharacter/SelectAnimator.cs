using UnityEngine;

public class SelectAnimator : MonoBehaviour
{

    void Start()
    {
        GetComponent<Animator>().SetTrigger("Start");
    }
}
