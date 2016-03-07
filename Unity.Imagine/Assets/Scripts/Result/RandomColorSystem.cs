using UnityEngine;
using System.Collections;

public class RandomColorSystem : MonoBehaviour
{
    void Update()
    {
        GetComponent<ParticleSystem>().startColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}
