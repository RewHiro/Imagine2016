using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Utility;

public class ConfigPanel : MonoBehaviour {

    const string LOAD_SCENE_NAME = "Menu";

    [SerializeField]
    private GameObject _notPrinterConfigPanel = null;
    [SerializeField]
    private GameObject _confirmationPanel = null;
    [SerializeField]
    private GameObject _captureCanvas = null;

    public void ClickButton()
    {
        _notPrinterConfigPanel.SetActive(false);
    }

    public void Return()
    {
        var screenSequencer = ScreenSequencer.instance;
        if (screenSequencer.isEffectPlaying) return;

        screenSequencer.SequenceStart
            (
                () => { GameScene.Menu.ChangeScene(); },
                new Fade(1.0f)
            );
    }

    public void ClickNoButton()
    {
        _confirmationPanel.SetActive(false);
        _captureCanvas.SetActive(true);
    }
	
}
