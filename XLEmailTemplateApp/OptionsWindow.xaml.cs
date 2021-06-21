using System.Windows;

namespace XLEmailTemplateApp
{

    public partial class OptionsWindow : Window
    {

        public OptionsWindow()
        {
            InitializeComponent();

            NameOptionCheckBox.IsChecked = (Options.hasNameOptionOn) ? true : false;
            NameOptionTextBlock.Text = $"hasNameOptionOn = {Options.hasNameOptionOn}";

            DefaultTextCheckBox.IsChecked = (Options.isShowingDefaultText) ? true : false;
            DefaultTextTextBlock.Text = $"isShowingDefaultText = {Options.isShowingDefaultText}";

            PreviewPaneCheckBox.IsChecked = (Options.isShowingPreviewPane) ? true : false;
            PreviewPaneTextBlock.Text = $"isShowingPreviewPane = {Options.isShowingPreviewPane}";

            DoubleColumnCheckBox.IsChecked = (Options.isShowingPreviewPane) ? true : false;
            DoubleColumnTextBlock.Text = $"isDoubleColumn = {Options.isDoubleColumn}";
        }


        private void DefaultTextCheckBox_Click(object sender, RoutedEventArgs e)
        {
            DefaultTextTextBlock.Text = "You really fucking did it, Brian even super stoned.";
            DefaultTextCheckBox.IsChecked = false;
        }

        private void DefaultTextCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Options.isShowingDefaultText = true;
            DefaultTextCheckBox.IsChecked = true;
            Options.SaveSettings();
        }

        private void PreviewPaneCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Options.isShowingPreviewPane = true;
            Options.SaveSettings();
        }

        private void NameOptionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Options.hasNameOptionOn = false;
            Options.SaveSettings();
        }

        private void DefaultTextCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Options.isShowingDefaultText = false;
            Options.SaveSettings();
        }

        private void PreviewPaneCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Options.isShowingPreviewPane = false;
            Options.SaveSettings();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }

        public void OpenMainWindow()
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            Close();
        }

        private void DoubleColumnCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Options.isDoubleColumn = true;
            Options.SaveSettings();
        }
    }
}
