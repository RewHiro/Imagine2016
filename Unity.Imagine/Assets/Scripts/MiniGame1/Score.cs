using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Score : MonoBehaviour {

    private Text _text;


    KeyAction _actionManager;

    List<GameObject> _playerList = new List<GameObject>();

    [SerializeField]
    StartCount _startCount;

    List<Barrage> _barragelist = new List<Barrage>();

    private enum Player
    {
        Player1,
        Player2
    }

    [SerializeField]
    Player _player = Player.Player1;

    void Start ()
    {
        if(_startCount == null)
        {
            _startCount = GameObject.Find("StartCount").GetComponent<StartCount>();
        }
        _actionManager = FindObjectOfType<KeyAction>();
        _text = GetComponent<Text>();
        
    }
	
	void Update ()
    {
        DrawScore();
    }

    void DrawScore()
    {
        
        if (_playerList.Count == 0 && _startCount.getCountFinish == true)
        {
            _playerList = _actionManager.GetPlayers();
            foreach (var selectPlayer in _playerList)
            {
                _barragelist.Add(selectPlayer.GetComponentInChildren<Barrage>());
            }

        }

        if (_playerList.Count == 0) return;
        //player[0].GetComponentInChildren<Barrage>()._getKeyCount; 
        // _text.text = "" + _activeModel.getBarrage._getKeyCount;
        if (_player == Player.Player1)
        {
            _text.text = "" + _barragelist[0]._getKeyCount;
            //_text.text = "" + _playerList[0].GetComponentInChildren<Barrage>()._getKeyCount;
        }
        else
        if (_player == Player.Player2)
        {
             _text.text = "" + _barragelist[1]._getKeyCount;
            //_text.text = "" + _playerList[1].GetComponentInChildren<Barrage>()._getKeyCount;
        }

    }
}
