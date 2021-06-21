namespace XLEmailTemplateApp
{
    static class Options
    {
        public static bool hasNameOptionOn { get; set; }
        public static bool isShowingDefaultText { get; set; }
        public static bool isShowingPreviewPane { get; set; }
        public static bool isDoubleColumn { get; set; }

        public static void LoadSettings()
        {
            hasNameOptionOn = Properties.Settings.Default.HasNameOptionOn;
            isShowingDefaultText = Properties.Settings.Default.IsShowingDefaultText;
            isShowingPreviewPane = Properties.Settings.Default.IsShowingPreviewPane;
            isDoubleColumn = Properties.Settings.Default.IsDoubleColumn;
        }

        public static void SaveSettings()
        {
            Properties.Settings.Default.HasNameOptionOn = hasNameOptionOn;
            Properties.Settings.Default.IsShowingDefaultText = isShowingDefaultText;
            Properties.Settings.Default.IsShowingPreviewPane = isShowingPreviewPane;
            Properties.Settings.Default.IsDoubleColumn = isDoubleColumn;
            Properties.Settings.Default.Save();
        }
    }
}
