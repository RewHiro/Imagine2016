
using System;
using Game.Models;

class Debugger {
  static void Main(string[] args) {
    Console.WriteLine("----- debugger start\n");
    var h = new hoge();
    h.f();
    Console.WriteLine("\n----- debugger finish");
  }
}
