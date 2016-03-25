using UnityEngine;
using System.Collections.Generic;

public class PlayerMaterialChange : MonoBehaviour
{

    [SerializeField]
    Material[] _material;

    List<GameObject> _playerList = new List<GameObject>();

    [SerializeField]
    KeyAction _keyAction;

    void Start()
    {
        _playerList = _keyAction.GetPlayers();
        //Debug.Log(_playerList.Count);
        // _material = new Material[4];
        if (_keyAction == null)
        {
            _keyAction = FindObjectOfType<KeyAction>();
        }
        //_material[0] = Resources.Load<Material>("Resources/MiniGame/Game1/Material/Clip");
        //_material[1] = Resources.Load<Material>("Resources/MiniGame/Game1/Material/RedCannonMaterial");
        //_material[2] = Resources.Load<Material>("Resources/MiniGame/Game1/Material/BlueClipMaterial");
        //_material[3] = Resources.Load<Material>("Resources/MiniGame/Game1/Material/BlueCannonMaterial");

    }

    void Update()
    {
        MaterialChange();
    }

    public void MaterialChange()
    {
        if (_playerList == null)
        {
            _playerList = _keyAction.GetPlayers();
            return;
        }



        if (gameObject.transform.parent.gameObject == _playerList[0])
        {
            gameObject.GetComponent<Renderer>().material = _material[0];
            gameObject.transform.parent.gameObject.GetComponent<Renderer>().material = _material[1];
        }
        else
    if (gameObject.transform.parent.gameObject == _playerList[1])
        {
            gameObject.GetComponent<Renderer>().material = _material[2];
            gameObject.transform.parent.gameObject.GetComponent<Renderer>().material = _material[3];
        }
    }

}
