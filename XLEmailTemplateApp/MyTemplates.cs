using System;
using System.IO;
using System.Collections.Generic;

namespace XLEmailTemplateApp
{
    public static class MyTemplates
    {
        public static string FolderPath { get; set; } = Environment.GetEnvironmentVariable("USERPROFILE") + @"\XLEmailTemplates\";
        public static bool IsNewVersion { get; set; } = false;
        public static List<Template> List { get; set; } = new List<Template>();

        public static void CreateFilePath()
        {
            Directory.CreateDirectory(FolderPath);
        }
        public static void Load()
        {

        }
        public static void Add(Template template)
        {
            List.Add(template);
        }
    }
}
