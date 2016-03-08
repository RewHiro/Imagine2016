using UnityEngine;
using UnityEngine.UI;
using Game.Utility;
using System;

public class PrinterConfig : MonoBehaviour
{
    const int PrinterName = 0;
    const int PrinterColor = 1;

    private Dropdown _data;
    private Dropdown.OptionData _item;

    [SerializeField, Tooltip("0:プリンター設定, 1:カラー設定")]
    private int _type = PrinterName;

    void Start()
    {
        _data = GetComponent<Dropdown>();
        if (_type == PrinterName)
        {
            if (!PrintDevice.isValid)
            {
                throw new NotSupportedException("認識できるプリンターがありません");
            }
            var printer = PrintDevice.GetPrinterNames().GetEnumerator();
            while (printer.MoveNext())
            {
                _item = new Dropdown.OptionData();
                _item.text = printer.Current;
                _data.options.Add(_item);
            }
            _data.captionText.text = _data.options[0].text;
            
        }
        else if(_type == PrinterColor)
        {
            if(_data.value == 0)
            {
                var color = PrintDevice.GetPrinterColorConfig(true);
            }
            else if(_data.value == 1)
            {
                var color = PrintDevice.GetPrinterColorConfig(false);
                Debug.Log(color);
            }
        }
    }

    /// <summary>
    /// カラーの設定
    /// モノクロならfalse,カラーならtrueにする
    /// </summary>
    public void ColorConfig()
    {
        if(_data.value == 0)
        {
            var color = PrintDevice.GetPrinterColorConfig(true);
            Debug.Log(color);
        }
        else if(_data.value == 1)
        {
            var color = PrintDevice.GetPrinterColorConfig(false);
            Debug.Log(color);
        }
        else
        {
            throw new IndexOutOfRangeException("Out of Range");
        }
    }

    
  
    void Update()
    {

    }
}
