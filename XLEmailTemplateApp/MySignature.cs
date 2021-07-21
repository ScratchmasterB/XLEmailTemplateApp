using System;
using System.IO;
using System.Xml;


namespace XLEmailTemplateApp
{
    public static class MySignature
    {
        public static string FolderPath { get; set; } = Environment.GetEnvironmentVariable("USERPROFILE")
            + @"\XLEmailTemplates\MySignature\";

        public static string FilePath { get; set; } = FolderPath + @"MySignature.xml";
        public static string DefaultText { get; set; } = "Your Name\r\nXL.net\r\nYour Job Title";
        public static string Text { get; set; } = "Default Signature";
        public static string ButtonBackground { get; set; }
        public static string ButtonForeground { get; set; }

        public static void Load()
        {
            Directory.CreateDirectory(FolderPath);
            if (File.Exists(FilePath)) Read();
            else Write();
        }

        public static void Read()
        {
            using (XmlReader read = XmlReader.Create(FilePath))
            {
                while (read.Read())
                {

                    if (read.IsStartElement())
                    {
                        switch (read.Name.ToString())
                        {
                            case "Text":
                                Text = read.ReadElementContentAsString();
                                break;
                            case "ButtonBackground":
                                ButtonBackground = read.ReadElementContentAsString();
                                break;
                            case "ButtonForeground":
                                ButtonForeground = read.ReadElementContentAsString();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            Write();
        }

        static public void Write()
        {
            NullCheck();

            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("MySignature");

            XmlElement text = doc.CreateElement("Text");
            text.InnerText = Text;
            root.AppendChild(text);

            XmlElement buttonBackground = doc.CreateElement("ButtonBackground");
            buttonBackground.InnerText = ButtonBackground;
            root.AppendChild(buttonBackground);

            XmlElement buttonForeground = doc.CreateElement("ButtonForeground");
            buttonForeground.InnerText = ButtonForeground;
            root.AppendChild(buttonForeground);

            doc.AppendChild(root);
            doc.Save(FilePath);
        }

        static public void NullCheck()
        {
            if (ButtonBackground == null) ButtonBackground = "#FF000000";
            if (ButtonForeground == null) ButtonForeground = "#FFf5ce42";
            if (String.IsNullOrWhiteSpace(Text)) Text = "Default Signature";
        }
    }


}
