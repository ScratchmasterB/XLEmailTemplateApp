using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;

namespace XLEmailTemplateApp
{
    class MyButtons
    {
        public static List<Button> List { get; set; } = new List<Button>();

        public static void Add(Button button)
        {
            List.Add(button);
        }
    }
}
