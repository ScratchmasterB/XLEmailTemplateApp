using System.Windows;

namespace XLEmailTemplateApp
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }

        public void OpenMainWindow()
        {
            var newWindow = new MainWindow();
            OpenNewWindow(newWindow);
        }

        public void OpenNewWindow(Window window)
        {
            window.WindowStartupLocation = WindowStartupLocation.Manual;
            window.Left = this.Left;
            window.Top = this.Top;
            window.Show();
            Close();
        }
    }
}
