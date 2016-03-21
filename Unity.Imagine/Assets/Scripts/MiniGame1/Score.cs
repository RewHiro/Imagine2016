using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private Text _text;

    [SerializeField]
    private ActiveModel _activeModel;

    void Start ()
    {
        _text = GetComponent<Text>();
	}
	
	void Update ()
    {
        _text.text = "" + _activeModel.getBarrage._getKeyCount;
    }
}
