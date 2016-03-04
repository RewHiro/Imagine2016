using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game.Utility;
using System.IO;

public class PrintStart : MonoBehaviour {

    [SerializeField, Tooltip("画像の名前")]
    private string _textureName;

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

        int width = 600;
        int height = 400;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        var pngData = tex.EncodeToPNG();
        var screenShotPath = GetScreenShotPath();
        File.WriteAllBytes(GetScreenShotPath(), pngData);
        
        var printer = GameObject.Find("Printer").GetComponent<Dropdown>();
        Debug.Log(printer.value);

        var color = GameObject.Find("Color").GetComponent<Dropdown>();
        Debug.Log(color.value);

        var path = Application.dataPath + "/Resources/" + _textureName + ".png";
        PrintDevice.PrintRequest(path, PrintDevice.DrawSize.one, printer.options[printer.value].text);

        Debug.Log(printer.options[printer.value].text);
        
    }

    private string GetScreenShotPath()
    {
        string path = "";
        path = "Craft.png";
        return path;
    }

}
