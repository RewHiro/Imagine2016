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

    /*
    //終わったかどうか
    [SerializeField]
    bool _isEnd;
    [SerializeField]
    int _winPlayerNum;
    */

    [SerializeField]
    GameObject _paperParticle;

    //Game終了後Setしたかどうか
    private bool _isSetPanels = false;

    public bool _isEnd { get; set; }

    public int _winPlayerNum { get; set; }

    void Start()
    {

    }

    void Update()
    {
        CheckISEnd();
    }

    private void CheckISEnd()
    {
        if (_isEnd == true && _isSetPanels == false)
        {
            SetPanels();
            SetPaperParticle();
            _isSetPanels = true;
        }
    }

    private void SetPanels()
    {
        if (_winPlayerNum == 2)
        {
            Vector3 _tempPos = _panelImage[0].transform.localPosition;
            _panelImage[0].transform.localPosition
                = new Vector3(_panelImage[1].transform.localPosition.x, _panelImage[1].transform.localPosition.y, _panelImage[1].transform.localPosition.z);

            _panelImage[1].transform.localPosition
                = new Vector3(_tempPos.x, _tempPos.y, _tempPos.z);
        }
    }

    private void SetPaperParticle()
    {
        if (_winPlayerNum == 2)
        {
            _paperParticle.transform.localPosition
                = new Vector3(-_paperParticle.transform.localPosition.x,
                _paperParticle.transform.localPosition.y,
                _paperParticle.transform.localPosition.z);
        }
    }

}
