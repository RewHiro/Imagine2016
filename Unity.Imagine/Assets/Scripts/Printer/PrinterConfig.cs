using UnityEngine;
using UnityEngine.UI;
using Game.Utility;
using System;

public class PrinterConfig : MonoBehaviour
{

    private Dropdown _data;
    private Dropdown.OptionData _item;

    [SerializeField]
    private bool _type = false;

    void Start()
    {
        _data = GetComponent<Dropdown>();
        if (_type == false)
        {
            var printer = PrintDevice.getPrinterNames().GetEnumerator();
            while (printer.MoveNext())
            {
                _item = new Dropdown.OptionData();
                _item.text = printer.Current;
                _data.options.Add(_item);
            }
        }
        else if(_type == true)
        {
            if(_data.value == 0)
            {
                var color = PrintDevice.getPrinterColorConfig(true);
                Debug.Log(color);
            }
            else if(_data.value == 1)
            {
                var color = PrintDevice.getPrinterColorConfig(false);
                Debug.Log(color);
            }
        }
    }


    public void ColorConfigo()
    {
        if(_data.value == 0)
        {
            var color = PrintDevice.getPrinterColorConfig(true);
            Debug.Log(color);
        }
        else if(_data.value == 1)
        {
            var color = PrintDevice.getPrinterColorConfig(false);
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
