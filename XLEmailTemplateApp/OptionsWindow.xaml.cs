using System.Windows;

namespace XLEmailTemplateApp
{

    public partial class OptionsWindow : Window
    {

        public OptionsWindow()
        {
            InitializeComponent();

            IanOptionCheckBox.IsChecked = MyPreferences.IsActive_IanOption;
            DefaultTextCheckBox.IsChecked = MyPreferences.IsShowingDefaultText;
        }


        private void IanOptionCheckBox_Click(object sender, RoutedEventArgs e)
        {
            MyPreferences.IsActive_IanOption = !MyPreferences.IsActive_IanOption;
            IanOptionCheckBox.IsChecked = MyPreferences.IsActive_IanOption;
            MyPreferences.Write();
        }

        private void DefaultTextCheckBox_Click(object sender, RoutedEventArgs e)
        {
            MyPreferences.IsShowingDefaultText = !MyPreferences.IsShowingDefaultText;
            DefaultTextCheckBox.IsChecked = MyPreferences.IsShowingDefaultText;
            MyPreferences.Write();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }

        public void OpenMainWindow()
        {
            var newWindow = new MainWindow();
            newWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            newWindow.Left = this.Left;
            newWindow.Top = this.Top;
            newWindow.Show();
            Close();
        }
    }
}
