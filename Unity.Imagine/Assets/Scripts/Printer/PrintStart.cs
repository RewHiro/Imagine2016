using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game.Utility;
using System.IO;

public class PrintStart : MonoBehaviour {

    [SerializeField, Tooltip("画像の名前")]
    private string _textureName;

    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;

    /// <summary>
    /// ボタンを押したらプリントスタート
    /// </summary>
    public void PrintingStart()
    {
        StartCoroutine(ScreenShot());
    }

    private IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();

        float x2 = (Screen.width / 1920.0f) * x;
        float y2 = (Screen.height / 1080.0f) * y;
        float w = (Screen.width / 1920.0f) * width;
        float h = (Screen.height / 1080.0f) * height;

        Texture2D tex = new Texture2D((int)w, (int)h, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(x2, y2, w, h), 0, 0);
        tex.Apply();

        var pngData = tex.EncodeToPNG();
        var screenShotPath = GetScreenShotPath();
        File.WriteAllBytes(screenShotPath, pngData);
        
        var printer = GameObject.Find("Printer").GetComponent<Dropdown>();
        Debug.Log(printer.value);

        var color = GameObject.Find("Color").GetComponent<Dropdown>();
        Debug.Log(color.value);

        //var path = Application.dataPath + "/Resources/" + _textureName + ".png";
        //Debug.Log(path);
        Debug.Log(screenShotPath);
        //PrintDevice.PrintRequest(screenShotPath, PrintDevice.DrawSize.one, printer.options[printer.value].text);

        Debug.Log(printer.options[printer.value].text);
        
    }

    private string GetScreenShotPath()
    {
        string path = "";
        path = Application.dataPath + "/Resources/Craft.png";
        return path;
    }

}
