using System.Globalization;
using System.Linq;
using System.Windows.Media;

namespace XLEmailTemplateApp
{
    static class Hexy
    {
        static public bool IsValidHexInput(string hexInput)
        {
            if (hexInput.IndexOf('#') == 0)
                hexInput = hexInput.Replace("#", "");

            if (hexInput.Length == 6 || hexInput.Length == 8)
            {
                string hexCharacters = "abcdefABCDEF1234567890";
                foreach (char c in hexInput)
                {
                    if (!hexCharacters.Contains(c)) return false;
                }
                return true;
            }
            else return false;
        }

        static public string CleanUpHex(string hexInput)
        {
            if (hexInput.IndexOf('#') == 0)
                hexInput = hexInput.Replace("#", "");
            if (hexInput.Length == 6) hexInput = "FF" + hexInput;
            return hexInput;
        }

        static public Brush HexToBrush(string hexString)
        {
            byte a, r, g, b = 0;

            a = (byte)int.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            r = (byte)int.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            g = (byte)int.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            b = (byte)int.Parse(hexString.Substring(6, 2), NumberStyles.AllowHexSpecifier);

            return new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }
    }
}
