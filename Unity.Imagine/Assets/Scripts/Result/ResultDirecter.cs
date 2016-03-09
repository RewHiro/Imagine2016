using UnityEngine;
using System.Collections;
/*
3/5
野本　変更開始

    GameMainからどちらかが勝ったのかをもらい
    その情報を元にPanelを入れ替えて描画を変えます。
    その時にActiveを入れて描画処理をしてもらうようにします。
*/

public class ResultDirecter : MonoBehaviour
{
    //2枚もらいます
    [SerializeField]
    GameObject[] _panelImage = null;

    //勝ったほうのPlayerの番号
    [SerializeField]
    int _winPlayerNum;

    public int winPlayerNum
    {
        get
        {
            return _winPlayerNum;
        }
    }

    [SerializeField]
    GameObject _paperParticle;

    [SerializeField]
    GameObject _buttonOfEndGame;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void SetResult(int winPlayerNum_)
    {
        SetPanelsActive(true);
        SetCanvas(winPlayerNum_);
    }

    public void SetPanelsActive(bool isActive_)
    {
        for (int i = 0; i < _panelImage.Length; ++i)
            _panelImage[i].SetActive(isActive_);

        _paperParticle.SetActive(isActive_);
        _buttonOfEndGame.SetActive(isActive_);
    }

    public void SetCanvas(int winPlayerNum_)
    {
        SetPanels(winPlayerNum_);
        SetPaperParticle(winPlayerNum_);
    }


    public void SetPanels(int winPlayerNum_)
    {
        if (winPlayerNum_ != 2) return;
        {
            Vector3 _tempPos = _panelImage[0].transform.localPosition;
            _panelImage[0].transform.localPosition
                = new Vector3(_panelImage[1].transform.localPosition.x, _panelImage[1].transform.localPosition.y, _panelImage[1].transform.localPosition.z);

            _panelImage[1].transform.localPosition
                = new Vector3(_tempPos.x, _tempPos.y, _tempPos.z);
        }
    }

    public void SetPaperParticle(int winPlayerNum_)
    {
        if (winPlayerNum_ == 2)
        {
            _paperParticle.transform.localPosition
                = new Vector3(-_paperParticle.transform.localPosition.x,
                _paperParticle.transform.localPosition.y,
                _paperParticle.transform.localPosition.z);
        }

        else if(winPlayerNum_ != 1 && winPlayerNum_ != 2)
        {
            Debug.Log("Warning! This number isn't PlayerNum");
        }
    }


}
