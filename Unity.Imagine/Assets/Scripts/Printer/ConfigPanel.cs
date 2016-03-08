using UnityEngine;
using Game.Utility;

public class ConfigPanel : MonoBehaviour {

    private GameObject _notPrinterConfigPanel = null;

    void Start()
    {
        _notPrinterConfigPanel = GameObject.Find("NotPrinterPanel");
    }

    public void ClickButton()
    {
        _notPrinterConfigPanel.SetActive(false);
    }
	
}
