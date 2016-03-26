using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReadyKey : MonoBehaviour {

    [SerializeField]
    ImageSizeChanger[] _myImages = null;

    [SerializeField]
    GameController controller = null;

    [SerializeField]
    UIMover _uiMover = null;

    Vector3 _drawPos = Vector3.zero;
    Vector3 _hidePos = new Vector3(0.0f, 1200.0f, 0.0f);

    bool _isReady = false;
    public bool isReady { get { return _isReady; } }

    [SerializeField]
    bool _debugFlag = false;

    void Update()
    {
        if (!_myImages[0].isDraw)
        {
            if (Input.GetKey("a"))
            {
                _myImages[0].ChangeSize();
            }
        }

        if (!_myImages[1].isDraw)
        {
            if (Input.GetKey("k"))
            {
                _myImages[1].ChangeSize();
            }
        }

        ReadyCheak();

        if (_debugFlag)
        {
            DrawReadyImage();
        }
        else
        {
            HideReadyImage();
        }
    }

    void ReadyCheak()
    {
        foreach (var image in _myImages)
        {
            if (!image.isDraw) { break; }
        }
        _isReady = true;
    }

    // これを呼べば描画される(描画位置に画像が飛んでいく)
    public void DrawReadyImage()
    {
        _uiMover.targetPos = _drawPos;
    }

    // これを呼べば描画されなくなる(画面外に画像が飛んでいく)
    public void HideReadyImage()
    {
        _uiMover.targetPos = _hidePos;
    }
}
