using System;
using System.IO;
using System.Xml;

namespace XLEmailTemplateApp
{
    static class MyPreferences
    {
        public static bool IsActive_IanOption { get; set; }
        public static bool IsShowingDefaultText { get; set; }
        public static string FolderPath { get; set; } = Environment.GetEnvironmentVariable("USERPROFILE")
            + @"\XLEmailTemplates\MyPreferences\";
        public static string FilePath { get; set; } = FolderPath + @"MyPreferences.xml";

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
                            case "IanOptionPreference":
                                if (read.ReadElementContentAsString() == "True") IsActive_IanOption = true;
                                else IsActive_IanOption = false;
                                break;
                            case "DefaultTextPreference":
                                if (read.ReadElementContentAsString() == "True") IsShowingDefaultText = true;
                                else IsShowingDefaultText = false;
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
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("MyPreferences");

            XmlElement ianOptionPreference = doc.CreateElement("IanOptionPreference");
            ianOptionPreference.InnerText = IsActive_IanOption.ToString();
            root.AppendChild(ianOptionPreference);

            XmlElement defaultTextPreference = doc.CreateElement("DefaultTextPreference");
            defaultTextPreference.InnerText = IsShowingDefaultText.ToString();
            root.AppendChild(defaultTextPreference);

            doc.AppendChild(root);
            doc.Save(FilePath);
        }
    }
}
