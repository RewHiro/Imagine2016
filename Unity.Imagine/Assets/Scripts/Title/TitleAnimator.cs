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
    Transform _orthoCamera = null;

    [SerializeField]
    GameObject[] _boxC = null;

    [SerializeField]
    GameObject _boxU = null;

    [SerializeField]
    GameObject[] _boxB = null;

    [SerializeField]
    GameObject _boxO = null;

    /// <summary>
    /// 色
    /// </summary>
    [SerializeField]
    Color _boxCColor = Color.red;

    [SerializeField]
    Color _boxUolor = Color.red;

    [SerializeField]
    Color _boxBColor = Color.red;

    [SerializeField]
    Color _boxOColor = Color.red;

    [SerializeField]
    Texture _boxOTexture = null;


    Animator _animator = null;

    List<Material> _materials = new List<Material>();
    List<Texture> _textures = new List<Texture>();

    AudioPlayer _audioPlayer = null;

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
            material.EnableKeyword("_Emission");
            material.SetColor("_EmissionColor", Color.black);

            material.SetFloat("_Mode", 1);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.EnableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 2450;

        }
        _materials[0].mainTexture = _boxOTexture;

        _plane.SetActive(false);
        _titleStartedDirector.SetActive(false);

        _audioPlayer = FindObjectOfType<AudioPlayer>();

        StartCoroutine(Animation());
        StartCoroutine(StartSE());
    }

    IEnumerator Animation()
    {
        const int C = 3;
        const int U = 1;
        const int B = 2;
        const int O = 0;

        const float START_TIME = 2.9f;
        const float LIMIT_POS_Y = -18.0f;

        yield return ChangeTexture(START_TIME + 0.0f, C, _boxCColor, _boxC);
        yield return ChangeTexture(START_TIME + 0.1f, U, _boxUolor, _boxU);
        _plane.SetActive(true);
        yield return ChangeTexture(START_TIME + 0.2f, B, _boxBColor,_boxB);
        yield return ChangeTexture(START_TIME + 0.3f, O, _boxOColor,_boxO);

        yield return new WaitForSeconds(2.0f);

        while (transform.position.y < LIMIT_POS_Y)
        {
            Debug.Log(transform.position);
            transform.Translate(0, SPEED, 0);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        _audioPlayer.Play(0, 1.0f, true);
        _titleStartedDirector.SetActive(true);
    }

    IEnumerator ChangeTexture
        (
        float offSetTime,
        int index,
        Color color,
        IEnumerable<GameObject> boxObjects
        )
    {
        while (_animator.GetTime() < offSetTime)
        {
            yield return null;
        }

        var enumerator = boxObjects.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            current.layer = LayerMask.NameToLayer("Viewer"); ;
        }

        var material = _materials[index];
        material.mainTexture = _textures[index];
        material.SetColor("_EmissionColor", color);
        material.SetFloat("_Mode", 2);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    IEnumerator ChangeTexture
    (
    float offSetTime,
    int index,
    Color color,
    GameObject boxObject
    )
    {
        while (_animator.GetTime() < offSetTime)
        {
            yield return null;
        }

        boxObject.layer = LayerMask.NameToLayer("Viewer");

        var material = _materials[index];
        material.mainTexture = _textures[index];
        material.SetColor("_EmissionColor", color);
        material.SetFloat("_Mode", 2);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    IEnumerator StartSE()
    {
        float count = 1.15f;

        while (count > 0)
        {
            count += -Time.deltaTime;
            yield return null;
        }

        _audioPlayer.Play(9);
    }
}
