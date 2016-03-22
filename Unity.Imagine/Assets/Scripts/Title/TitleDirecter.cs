using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirecter : MonoBehaviour
{

    const string LOAD_SCENE_NAME = "Menu";

    [SerializeField]
    AudioPlayer _player = null;

    //移動する雲
    [SerializeField]
    GameObject[] _cloud = null;
    //雲の移動速度
    private float _moveCloudSpeed = 60.0f;


    void Start()
    {
      
    }


    void Update()
    {
        //雲のUpdate
        UpdateOfClouds();
    }


    private void UpdateOfClouds()
    {
        foreach (GameObject gameObject in _cloud)
        {
            //移動処理
            gameObject.transform.localPosition =
                new Vector3(gameObject.transform.localPosition.x + _moveCloudSpeed * Time.deltaTime,
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
        _player.Stop();
        _player.Play(6, 1.0f, false);
        //StartButtonが押されたら
        var screenSequencer = ScreenSequencer.instance;

        screenSequencer.SequenceStart
            (
                () => { GameScene.Menu.ChangeScene(); },
                new Fade(1.0f)
            );
    }
}
