using UnityEngine;
using System.Collections;

public class AnimeterTest : MonoBehaviour
{
    [SerializeField]
    bool _isPlay;

    public bool isPlay
    {
        get
        {
            return _isPlay;
        }

        set
        {
            _isPlay = value;
        }
    }

    Animator _animator = null;

    private bool _isStarted = false;

    void Start()
    {

        _animator = GetComponent<Animator>();
        //animator.SetFloat("PlaySpeed", -1);
        //animator.playbackTime = 2;

    }

    void Update()
    {
        if(_isPlay == true && _isStarted == false)
        {
            _animator.SetTrigger("isPlay");
            _isPlay = false;
            _isStarted = true;
        }
    }
}
