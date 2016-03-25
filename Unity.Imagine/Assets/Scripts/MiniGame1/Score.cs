
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Score : MonoBehaviour
{

    private Text _text;


    KeyAction _actionManager;

    List<GameObject> _playerList = new List<GameObject>();

    [SerializeField]
    StartCount _startCount;

    List<Barrage> _barragelist = new List<Barrage>();

    ScoreCompare _scoreCompare;

    [SerializeField]
    TimeCount _timeCount;

    private enum Player
    {
        Player1,
        Player2
    }

    [SerializeField]
    Player _player = Player.Player1;

    void Start()
    {
        _scoreCompare = FindObjectOfType<ScoreCompare>();

        if (_startCount == null)
        {
            _startCount = GameObject.Find("StartCount").GetComponent<StartCount>();
        }

        if (_timeCount == null)
        {
            _timeCount = GameObject.Find("Time").GetComponent<TimeCount>();
        }

        _actionManager = FindObjectOfType<KeyAction>();
        _text = GetComponent<Text>();

    }

    void Update()
    {
        DrawScore();
    }

  public  void DrawScore()
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
        if (_player == Player.Player1)
        {
            _text.text = "" + _barragelist[0]._getKeyCount;

            if (_scoreCompare.getDisplayScore || _scoreCompare.getIsDraw) return;

            if (_timeCount._getTime <= 4)
            {
                _text.text = "???";
            }
         
        }
        else
        if (_player == Player.Player2)
        {
            _text.text = "" + _barragelist[1]._getKeyCount;
         
            if (_scoreCompare.getDisplayScore || _scoreCompare.getIsDraw) return;

            if (_timeCount._getTime <= 4)
            {
                _text.text = "???";
            }
        }

    }
}
