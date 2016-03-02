using UnityEngine;
using System.Collections;

public class Barrage : MonoBehaviour {

    [SerializeField]
    private KeyCode _key;


    [SerializeField]
    private int _keyCount = 0;

    public int _getKey { get { return _keyCount; } }

    void Start ()
    {

	}
	
	void Update ()
    {
        _keyCount += Barragebutton(_key);
        Debug.Log(Barragebutton(_key));
    }

    int Barragebutton(KeyCode key)
        {
        if(Input.GetKeyDown(key))
        {
            return 1;
        }

        return 0;
        }
}
