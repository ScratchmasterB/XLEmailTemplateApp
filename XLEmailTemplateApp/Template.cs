using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Path = System.IO.Path;

namespace XLEmailTemplateApp
{
    public class Template
    {
        public string TemplateName { get; set; }
        public string Greeting { get; set; }
        public string Addressee { get; set; }
        public string Body { get; set; }
        public string Closing { get; set; }

        public string FolderPath { get; set; } = MyTemplates.FolderPath;
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ButtonName { get; set; }

        public string ButtonBackgroundColor { get; set; }
        public string ButtonForegroundColor { get; set; }
        public string ButtonGroup { get; set; }
        public int ButtonOrder { get; set; }




        public Template()
        {

        }

        public Template(string fileName)
        {
            FileName = fileName;
            FilePath = FolderPath + FileName;
        }

        public bool CheckIfFileExists()
        {
            if (File.Exists(FilePath)) return true;
            return false;
        }

        public void SetButtonName()
        {
            var buttonName = new StringBuilder();
            foreach (char c in TemplateName)
            {
                if ((c > 64 && c < 91) || (c > 96 && c < 123) || (c > 47 && c < 58) || c == 95)
                {
                    buttonName.Append(c);
                }
            }
            ButtonName = buttonName.ToString();
        }

        public void SetTemplateFilePath()
        {
            var fileName = new StringBuilder();
            foreach (char c in TemplateName)
            {
                if ((c > 64 && c < 91) || (c > 96 && c < 123) || (c > 47 && c < 58) || c == 95)
                {
                    fileName.Append(c);
                }
            }
            FileName = fileName.ToString() + ".xml";
            FilePath = MyTemplates.FolderPath + FileName;
        }

        public bool IsFilledOutProperly()
        {
            if (string.IsNullOrWhiteSpace(TemplateName)) return false;
            else if (string.IsNullOrWhiteSpace(FileName)) return false;
            else return true;
        }





        public void AlterGreetingForTimeOfDay()
        {
            string timeOfDay = "morning";

            if (DateTime.Now.Hour >= 17) timeOfDay = "evening";
            else if (DateTime.Now.Hour >= 12) timeOfDay = "afternoon";

            Greeting = Regex.Replace(Greeting, @"\bmorning|afternoon|evening\b", timeOfDay, RegexOptions.IgnoreCase);
        }

        public void AlterClosingForDayOfWeek()
        {
            Closing = Regex.Replace(Closing, @"\bMonday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday\b", DateTime.Now.DayOfWeek.ToString(), RegexOptions.IgnoreCase);
        }

        public static void Delete(string templateName)
        {
            File.Delete(Path.Combine(MyTemplates.FolderPath, templateName));    //the only way this will work is if you move all other File manipulation to use Template.UserDirectory
        }

        public void WriteTemplateToFile()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("Template");

            XmlElement templateName = doc.CreateElement("TemplateName");
            templateName.InnerText = TemplateName;
            root.AppendChild(templateName);

            XmlElement greeting = doc.CreateElement("Greeting");
            greeting.InnerText = Greeting;
            root.AppendChild(greeting);

            XmlElement addressee = doc.CreateElement("Addressee");
            addressee.InnerText = Addressee;
            root.AppendChild(addressee);

            XmlElement body = doc.CreateElement("Body");
            body.InnerText = Body;
            root.AppendChild(body);

            XmlElement closing = doc.CreateElement("Closing");
            closing.InnerText = Closing;
            root.AppendChild(closing);

            XmlElement buttonBackgroundColor = doc.CreateElement("ButtonBackgroundColor");
            buttonBackgroundColor.InnerText = ButtonBackgroundColor;
            root.AppendChild(buttonBackgroundColor);

            XmlElement buttonForegroundColor = doc.CreateElement("ButtonForegroundColor");
            buttonForegroundColor.InnerText = ButtonForegroundColor;
            root.AppendChild(buttonForegroundColor);

            XmlElement buttonGroup = doc.CreateElement("ButtonGroup");
            buttonGroup.InnerText = ButtonGroup;
            root.AppendChild(buttonGroup);

            XmlElement buttonOrder = doc.CreateElement("ButtonOrder");
            buttonOrder.InnerText = ButtonOrder.ToString();
            root.AppendChild(buttonOrder);

            doc.AppendChild(root);
            doc.Save(FilePath);
        }

        public void InsertAddresseeIntoTemplate(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Addressee = Addressee.Replace("XXXX", name);
                Closing = Closing.Replace("XXXX", name);
            }
        }

        public void ReadXmlTemplate()
        {
            using (XmlReader read = XmlReader.Create(FilePath))
            {
                while (read.Read())
                {
                    if (read.IsStartElement())
                    {
                        switch (read.Name.ToString())
                        {
                            case "TemplateName":
                                TemplateName = read.ReadElementContentAsString();
                                break;
                            case "Greeting":
                                Greeting = read.ReadElementContentAsString();
                                break;
                            case "Addressee":
                                Addressee = read.ReadElementContentAsString();
                                break;
                            case "Body":
                                Body = read.ReadElementContentAsString();
                                break;
                            case "Closing":
                                Closing = read.ReadElementContentAsString();
                                break;
                            case "ButtonBackgroundColor":
                                ButtonBackgroundColor = read.ReadElementContentAsString();
                                break;
                            case "ButtonForegroundColor":
                                ButtonForegroundColor = read.ReadElementContentAsString();
                                break;
                            case "ButtonGroup":
                                ButtonGroup = read.ReadElementContentAsString();
                                break;
                            case "ButtonOrder":
                                ButtonOrder = read.ReadElementContentAsInt();
                                break;
                        }
                    }
                }

            }
            if (ButtonBackgroundColor == null || ButtonForegroundColor == null || String.IsNullOrWhiteSpace(ButtonGroup))
            {
                AssignDefaultValues();
                WriteTemplateToFile();
            }
        }
        public void AssignDefaultValues()
        {
            ButtonBackgroundColor = "#000000";
            ButtonForegroundColor = "#f5ce42";
            ButtonGroup = "Unassigned";
            ButtonOrder = 0;
        }
    }
}
