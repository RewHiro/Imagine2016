using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextureChange : MonoBehaviour {

    [SerializeField]
    private GameObject[] _panel;

    [SerializeField]
    private Sprite _backGroundTexture;

	void Start ()
    {
        foreach(var panel in _panel)
        {
            panel.GetComponent<Image>().sprite = _backGroundTexture;
        }
    }
	
	
}
