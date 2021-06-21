using System;
using System.IO;

namespace XLEmailTemplateApp
{
    public static class MyTemplates
    {
        public static string FolderPath { get; set; } = Environment.GetEnvironmentVariable("USERPROFILE") + @"\XLEmailTemplates\";
        public static bool isNewVersion { get; set; }
        public static void CreateEmailTemplatesFolderPath()
        {
            Directory.CreateDirectory(FolderPath);
        }
    }
}
