using System.Windows;

namespace XLEmailTemplateApp
{
    /// <summary>
    /// Interaction logic for HowToUseWindow.xaml
    /// </summary>
    public partial class HowToUseWindow : Window
    {
        public HowToUseWindow()
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
            newWindow.Show();
            Close();
        }
    }


}
