
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;

namespace Game.Utility
{

    public static class PrintDevice
    {
        public class DrawSize
        {
            public DrawSize(float width, float height)
            {
                this.width = width;
                this.height = height;
            }

            public static readonly DrawSize one = new DrawSize(1f, 1f);
            public float width { get; private set; }
            public float height { get; private set; }

            public static DrawSize operator *(DrawSize s, float mul)
            {
                return new DrawSize(s.width * mul, s.height * mul);
            }
        }

        public static bool PrintRequest(string path)
        {
            if (!BindImage(path)) { return false; }
            PrintImage();
            _image.Dispose();
            _image = null;
            return true;
        }

        public static bool PrintRequest(string path, DrawSize size)
        {
            PrintDevice.size = size;
            return PrintRequest(path);
        }

        static Image _image = null;
        public static DrawSize size { get; set; }

        static bool BindImage(string path)
        {
            _image = Image.FromFile(path);
            return _image != null;
        }

        static void PrintImage()
        {
            var pd = new PrintDocument();
            pd.DocumentName = "ar marker";
            pd.PrintPage += new PrintPageEventHandler(PrintEventAction);
            pd.Print();
        }

        static void PrintEventAction(object sender, PrintPageEventArgs args)
        {
            var image = _image;
            var margin = args.MarginBounds;
            args.Graphics.DrawImage(image, margin.Left, margin.Top, size.width, size.height);
            args.HasMorePages = false;
            image.Dispose();
        }

        public static IEnumerable<string> getPrinterNames()
        {
            foreach (string name in PrinterSettings.InstalledPrinters)
            {
                yield return name;
            }
        }

        public static bool getPrinterColorConfig(bool type)
        {
            var printer = new PrinterSettings();
            var pageSettings = new PageSettings(printer);
            pageSettings.Color = type;
            return pageSettings.Color;
        }

    }
}
