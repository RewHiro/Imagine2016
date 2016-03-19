using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {

    private Text _text;

    [SerializeField]
    StartCount _startCount;

    [SerializeField]
    private int _timeCount = 10;

    float _time = 0;

   public float  _getTime { get {return _time; }}

	void Start ()
    {
        if (_startCount == null)
        {
            _startCount = GameObject.Find("StartCount").GetComponent<StartCount>();
        }
        _time = _timeCount;
        _text = GetComponent<Text>();
    }
	
	void Update ()
    {
        _text.text = "Time : " + (int)_time;
        if (_startCount.getCountFinish)
        {
            _time = TimeLimit(_time);
        }

    }

    public float  TimeLimit(float count)
    {
        if (count <= 0) return 0;
        count -= Time.deltaTime;

        return count;
    }

}
