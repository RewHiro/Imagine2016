using UnityEngine;
using System.Collections;

public class MenuBoxAnimater : MonoBehaviour
{
    [SerializeField]
    bool _isPlay;
    [SerializeField]
    bool _isBack;
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

    public bool isBack
    {
        get
        {
            return _isBack;
        }

        set
        {
            _isBack = value;
        }
    }


    Animator _animator = null;

    private bool _isStarted = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isPlay == true && _isStarted == false)
        {
            _animator.SetTrigger("isPlay");
            _isPlay = false;
            _isStarted = true;
        }

        if (_isBack == true && _isStarted == true)
        {
            _animator.SetTrigger("isBack");
            _isBack = false;
            _isPlay = false;
            _isStarted = false;
        }
    }

}
