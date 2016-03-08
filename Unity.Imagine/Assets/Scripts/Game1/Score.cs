using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private Text _text;

    [SerializeField]
    private Barrage _barrago;

    void Start ()
    {
        _text = GetComponent<Text>();
	}
	
	void Update ()
    {
        _text.text = "Score : " + _barrago._getKeyCount;
    }
}
