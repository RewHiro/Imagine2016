using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryOrDefeat : MonoBehaviour {

    ScoreCompare _scoreCompare;

    [SerializeField]
    Image[] _images;

    ReturnMenu _returnMenu;

    ResultDirecter _resultDirecter;

    bool isResult = false;

    void Start ()
    {
        _resultDirecter = FindObjectOfType<ResultDirecter>();
        _returnMenu = FindObjectOfType<ReturnMenu>();
        _scoreCompare = FindObjectOfType<ScoreCompare>();
    }
	
	void Update ()
    {
        if (_scoreCompare.getDisplayScore == true)
        {
            DrawImage();
        }
    }

    void DrawImage()
    {
        if (_returnMenu.getIsRotationEnd == false) return;

        if (_scoreCompare.getWinPlayer == ScoreCompare.WinPlayer.Player1)
        {
            //if (isResult == false)
            //{
            //    _resultDirecter.SetResult(1);
            //}
            _images[0].enabled = true;
            _images[3].enabled = true;
            isResult = true;
        }else
                if (_scoreCompare.getWinPlayer == ScoreCompare.WinPlayer.Player2)
        {
            //if (isResult == false)
            //{
            //    _resultDirecter.SetResult(2);
            //}
            _images[1].enabled = true;
            _images[2].enabled = true;
            isResult = true;
        }

    }
}
