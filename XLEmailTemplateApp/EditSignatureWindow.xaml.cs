using System.Windows;
using System.Windows.Controls;

namespace XLEmailTemplateApp
{
    public partial class EditSignatureWindow : Window
    {
        public EditSignatureWindow()
        {
            InitializeComponent();

            MySignature.Load();
            SignatureTextBox.Text = MySignature.Text;
            ButtonBackgroundTextBox.Text = MySignature.ButtonBackground;
            ButtonForegroundTextBox.Text = MySignature.ButtonForeground;
        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MySignature.Text = SignatureTextBox.Text;
            MySignature.ButtonBackground = ButtonBackgroundTextBox.Text;
            MySignature.ButtonForeground = ButtonForegroundTextBox.Text;
            MySignature.Write();
            OpenMainWindow();
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

        private void ButtonBackgroundTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Hexy.IsValidHexInput(ButtonBackgroundTextBox.Text))
                ExampleButton.Background = Hexy.HexToBrush(Hexy.CleanUpHex(ButtonBackgroundTextBox.Text));
        }

        private void ButtonForegroundTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Hexy.IsValidHexInput(ButtonForegroundTextBox.Text))
                ExampleButton.Foreground = Hexy.HexToBrush(Hexy.CleanUpHex(ButtonForegroundTextBox.Text));
        }
    }
}
