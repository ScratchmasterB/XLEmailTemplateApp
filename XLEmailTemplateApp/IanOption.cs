using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;
using System.Runtime;

namespace XLEmailTemplateApp
{
    static class IanOption
    {
        private static List<string> hexColors = new List<string>()
        {
            "#FF33FF", "#00FFFF", "#FFFF00", "#FF00FF", "#66FF66", "#5555FF", "#00CCCC", "#FFFF00"
        };

        private static int index = 0;

        public static string GetNextHex()
        {
            if (index >= hexColors.Count) index = 0;
            return hexColors[index++];
        }

        public static ImageBrush SetBackground()
        {
            ImageBrush imageBrush = new ImageBrush();
            Image image = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(@"pack://application:,,,/Images/IanOptionBG.png");
            bitmapImage.EndInit();
            image.Source = bitmapImage;
            imageBrush.ImageSource = image.Source;
            return imageBrush;
        }

        public static void SetButtonColors(Panel panel)
        {
            foreach (UIElement element in panel.Children)
            {
                var background = Hexy.HexToBrush(Hexy.CleanUpHex(GetNextHex()));
                var foreground = Hexy.HexToBrush(Hexy.CleanUpHex(GetNextHex()));

                element.GetType().GetProperty("Background").SetValue(element, background);
                element.GetType().GetProperty("Foreground").SetValue(element, foreground);
            }
        }

        public static void SetMenuColors(Menu menu)
        {
            menu.Foreground = Hexy.HexToBrush(Hexy.CleanUpHex("#00FF00"));
        }


    }
}
