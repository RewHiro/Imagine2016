//using UnityEngine;
//using UnityEngine.UI;
//using Game.Utility;
//using System;

//public class PrinterConfig : MonoBehaviour
//{
//    const int PRINTER_NAME = 0;
//    const int PRINTER_COLOR = 1;

//    private Dropdown _data;
//    private Dropdown.OptionData _item;

//    [SerializeField, Tooltip("0:プリンター設定, 1:カラー設定")]
//    private int _type = PRINTER_NAME;

  
//    void Start()
//    {
//        _data = GetComponent<Dropdown>();
//        if (_type == PRINTER_NAME)
//        {
//            var printer = PrintDevice.getPrinterNames().GetEnumerator();
//            while (printer.MoveNext())
//            {
//                _item = new Dropdown.OptionData();
//                _item.text = printer.Current;
//                _data.options.Add(_item);
//            }
//        }
//        else if(_type == PRINTER_COLOR)
//        {
//            if(_data.value == 0)
//            {
//                var color = PrintDevice.getPrinterColorConfig(true);
//            }
//            else if(_data.value == 1)
//            {
//                var color = PrintDevice.getPrinterColorConfig(false);
//                Debug.Log(color);
//            }
//        }
//    }

//    /// <summary>
//    /// カラーの設定
//    /// モノクロならfalse,カラーならtrueにする
//    /// </summary>
//    public void ColorConfig()
//    {
//        if(_data.value == 0)
//        {
//            var color = PrintDevice.getPrinterColorConfig(true);
//            Debug.Log(color);
//        }
//        else if(_data.value == 1)
//        {
//            var color = PrintDevice.getPrinterColorConfig(false);
//            Debug.Log(color);
//        }
//        else
//        {
//            throw new IndexOutOfRangeException("Out of Range");
//        }
//    }

    
  
//    void Update()
//    {

//    }
//}
