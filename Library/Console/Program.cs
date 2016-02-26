
using System;
using Game.Models;
using Game.Utility;

class Debugger {
  static void Main(string[] args) {
    Console.WriteLine("----- debugger start\n");

    var size = PrintDevice.DrawSize.one * 100f;
    Console.WriteLine(" width = " + size.width);
    Console.WriteLine("height = " + size.height);

    Console.WriteLine("\n----- debugger finish");
  }
}
