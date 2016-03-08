using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Utility;

public class ConfigPanel : MonoBehaviour {

    const string LOAD_SCENE_NAME = "Menu";

    private GameObject _notPrinterConfigPanel = null;

    void Start()
    {
        _notPrinterConfigPanel = GameObject.Find("NotPrinterPanel");
    }

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
                () => { SceneManager.LoadScene(LOAD_SCENE_NAME); },
                new Fade(1.0f)
            );
    }
	
}
