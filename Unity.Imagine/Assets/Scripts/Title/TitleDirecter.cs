using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirecter : MonoBehaviour
{

    const string LOAD_SCENE_NAME = "Menu";

    [SerializeField]
    GameObject _logo = null;
    //Easingで掛ける距離
    private float _s = 1.70158f * 1.525f;
    //Eaisngしている合計時間
    private float _easingTime = 0.0f;
    //Easing関数
    private float EasingBackInOut(float time_, float beginPos_, float endPos_)
    {
        if ((time_ /= 0.5f) < 1.0f) return (endPos_ - beginPos_) / 2 * (time_ * time_ * ((_s + 1) * time_ - _s)) + beginPos_;
        time_ -= 2;
        return (endPos_ - beginPos_) / 2 * (time_ * time_ * ((_s + 1) * time_ + _s) + 2) + beginPos_;
    }

    //移動する雲
    [SerializeField]
    GameObject[] _cloud = null;
    //雲の移動速度
    private float _moveCloudSpeed = 6.0f;

    void Start()
    {

    }


    void Update()
    {
        //NameLogoのUpdate
        //UpdateOfNameLogo();
        //雲のUpdate
        UpdateOfClouds();
    }

    private void UpdateOfNameLogo()
    {
        //Easingを40Countで行う
        if (_easingTime < 1.0f)
            _easingTime += 1.0f / 40.0f;
        //local座標の移動
        _logo.transform.localPosition = new Vector3(-460, EasingBackInOut(_easingTime, 1200, 400), 0);
    }


    private void UpdateOfClouds()
    {
        foreach (GameObject gameObject in _cloud)
        {
            //移動処理
            gameObject.transform.localPosition =
                new Vector3(gameObject.transform.localPosition.x + _moveCloudSpeed,
                            gameObject.transform.localPosition.y,
                            gameObject.transform.localPosition.z);

            //範囲の外に出たら左側に戻す
            if (gameObject.transform.localPosition.x <= 1100)
                continue;

            gameObject.transform.localPosition =
            new Vector3(gameObject.transform.localPosition.x - 2400,
                        gameObject.transform.localPosition.y,
                        gameObject.transform.localPosition.z);
        }
    }

    public void PushStartButton()
    {
        //StartButtonが押されたら
        var screenSequencer = ScreenSequencer.instance;

        screenSequencer.SequenceStart
            (
                () => { SceneManager.LoadScene(LOAD_SCENE_NAME); },
                new Fade(1.0f)
            );
    }
}
