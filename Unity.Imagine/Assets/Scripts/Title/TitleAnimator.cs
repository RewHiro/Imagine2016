using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleAnimator : MonoBehaviour
{

    [SerializeField, Range(0.1f, 10.0f)]
    float SPEED = 0.1f;

    [SerializeField]
    GameObject _plane = null;

    [SerializeField]
    GameObject _titleStartedDirector = null;

    [SerializeField]
    GameObject _orthoCamera = null;

    Animator _animator = null;

    List<Material> _materials = new List<Material>();
    List<Texture> _textures = new List<Texture>();

    void Start()
    {
        _animator = GetComponent<Animator>();

        const string PATH = "Title/Animation/";

        _materials.AddRange
            (
                Resources.LoadAll<Material>(PATH + "Materials")
            );

        _textures.AddRange
            (
                Resources.LoadAll<Texture>(PATH + "Textures")
            );

        foreach (var material in _materials)
        {
            material.mainTexture = new Texture();
        }

        _plane.SetActive(false);
        _titleStartedDirector.SetActive(false);

        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        const int C = 3;
        const int U = 1;
        const int B = 2;
        const int O = 0;

        const float START_TIME = 2.9f;
        const float LIMIT_POS_Y = -20.0f;

        yield return ChangeTexture(START_TIME + 0.0f, C);
        yield return ChangeTexture(START_TIME + 0.1f, U);
        _plane.SetActive(true);
        yield return ChangeTexture(START_TIME + 0.2f, B);
        yield return ChangeTexture(START_TIME + 0.3f, O);

        yield return new WaitForSeconds(2.0f);

        while (_orthoCamera.transform.position.y > LIMIT_POS_Y)
        {
            _orthoCamera.transform.Translate(0, -SPEED, 0);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        _titleStartedDirector.SetActive(true);
    }

    IEnumerator ChangeTexture(float offSetTime, int index)
    {
        while (_animator.GetTime() < offSetTime)
        {
            yield return null;
        }

        _materials[index].mainTexture = _textures[index];
    }
}
