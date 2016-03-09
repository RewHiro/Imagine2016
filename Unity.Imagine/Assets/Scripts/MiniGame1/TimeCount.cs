using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {

    private Text _text;

    [SerializeField]
    private int _timeCount = 10;

    float _Time = 0;

   public float  _getTime { get {return _Time; }}

	void Start ()
    {
        _Time = _timeCount;
        _text = GetComponent<Text>();
    }
	
	void Update ()
    {
        _Time = TimeLimit(_Time);
        _text.text = "Time : " +(int) _Time;

    }

    public float  TimeLimit(float count)
    {
        if (count <= 0)
            return 0;
        count -= Time.deltaTime;

        return count;
    }

}
