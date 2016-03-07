using UnityEngine;
using Game.Utility;

public class ConfigPanel : MonoBehaviour {

    private GameObject _NotPrinterConfigPanel = null;

    void Start()
    {
        _NotPrinterConfigPanel = GameObject.Find("NotPrinterPanel");
    }

    public void ClickButton()
    {
        _NotPrinterConfigPanel.SetActive(false);
    }
	
}
