using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReturnMenu : MonoBehaviour {

    [SerializeField]
    private LayerMask _mask;

    [SerializeField]
    private TimeCount _timeCount = null;

    [SerializeField]
    Text _text = null;

    [SerializeField]
    float _turnAngle = 360.0f;
    [SerializeField]
    float _turnSpeed = 20.0f;

    bool isTurn = false;
    bool isReturn = false;
	
	// Update is called once per frame
	void Update () {
        // この１文はタイムが０になったら起こるようにしてあるので
        // 必要なかったり、別のところで必要になったら消してください。
        if (_timeCount._getTime <= 0) { ReturnOn(); }


        Turn();
        ReturnMenuUpdate();
    }

    void Turn()
    {
        if (isTurn)
        {
            _turnAngle -= _turnSpeed;
            if (_turnAngle < 0.0f)
            {
                isTurn = false;
                isReturn = true;
                _turnAngle = 0.0f;
            }
            transform.eulerAngles = new Vector3(0.0f, _turnAngle, 0.0f);
        }
    }

    void ReturnMenuUpdate()
    {
        if (!isReturn) { return; }
        if (_timeCount._getTime > 0) { return; }

        if (_timeCount != null) { Destroy(_timeCount); }

        _text.text = "メニューに戻る";

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;
            if (TouchController.IsRaycastHitWithLayer(out hit, _mask))
            {
                if (hit.transform.name == transform.name)
                {
                    GameScene.Menu.ChangeScene();
                }
            }
        }
    }

    // これを呼べばメニューに戻る
    public void ReturnOn()
    {
        isTurn = true;
    }
}
