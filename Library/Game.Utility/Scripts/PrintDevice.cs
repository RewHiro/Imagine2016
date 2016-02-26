
using System.Drawing;
using System.Drawing.Printing;

namespace Game.Utility {

  public static class PrintDevice {
    static Image _image = null;

    static bool BindImage(string path) {
      _image = Image.FromFile(path);
      return _image != null;
    }

    static void PrintImage() {
      var pd = new PrintDocument();
      pd.DocumentName = "ar marker";
      pd.PrintPage += new PrintPageEventHandler(PrintEventAction);
      pd.Print();
    }

    public static bool PrintRequest(string path) {
      if (!BindImage(path)) { return false; }
      PrintImage();
      _image = null;
      return true;
    }

    static void PrintEventAction(object sender, PrintPageEventArgs args) {
      var image = _image;
      args.Graphics.DrawImage(image, args.MarginBounds);
      args.HasMorePages = false;
      image.Dispose();
    }
  }
}
