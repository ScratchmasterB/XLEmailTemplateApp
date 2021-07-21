using System.Windows;
using System.Windows.Controls;

namespace XLEmailTemplateApp
{
    public partial class EditTemplateWindow : Window
    {
        public string InitialTemplateName { get; set; }


        public EditTemplateWindow(Button button)
        {

            InitializeComponent();

            var template = new Template(button.Name + ".xml");
            template.ReadXmlTemplate();

            if (MyTemplates.IsNewVersion)
            {
                Title = "New Version";
            }

            TemplateNameTextBox.Text = template.TemplateName;
            GreetingTextBox.Text = template.Greeting;
            AddresseeTextBox.Text = template.Addressee;
            BodyTextBox.Text = template.Body;
            ClosingTextBox.Text = template.Closing;
            SignatureTextBox.Text = MySignature.Text;
            ButtonBackgroundColorTextBox.Text = template.ButtonBackgroundColor;
            ButtonForegroundColorTextBox.Text = template.ButtonForegroundColor;

            InitialTemplateName = template.TemplateName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (InitialTemplateName == TemplateNameTextBox.Text && MyTemplates.IsNewVersion)
            {
                MessageBox.Show("New template version must have a different name", "Error message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrWhiteSpace(TemplateNameTextBox.Text))
            {
                MessageBox.Show("Template must have a name", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!Hexy.IsValidHexInput(ButtonBackgroundColorTextBox.Text) || !Hexy.IsValidHexInput(ButtonForegroundColorTextBox.Text))
            {

            }
            else
            {
                Hexy.CleanUpHex(ButtonBackgroundColorTextBox.Text);
                Hexy.CleanUpHex(ButtonForegroundColorTextBox.Text);
                SaveTemplateToFile();

                OpenMainWindow();
            }
        }

        void OpenMainWindow()
        {
            var newWindow = new MainWindow();
            newWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            newWindow.Left = this.Left;
            newWindow.Top = this.Top;
            newWindow.Show();
            Close();
        }
        private void SaveTemplateToFile()
        {

            var template = new Template()
            {
                TemplateName = TemplateNameTextBox.Text,
                Greeting = GreetingTextBox.Text,
                Addressee = AddresseeTextBox.Text,
                Body = BodyTextBox.Text,
                Closing = ClosingTextBox.Text,
                ButtonBackgroundColor = ButtonBackgroundColorTextBox.Text,
                ButtonForegroundColor = ButtonForegroundColorTextBox.Text,
                ButtonGroup = "Assigned",
                ButtonOrder = 5
            };

            if (SignatureTextBox.Text != MySignature.Text)
            {
                MySignature.Text = SignatureTextBox.Text;
                // UpdateSignature()
            }

            template.SetTemplateFilePath();
            template.WriteTemplateToFile();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }

        private void ButtonBackgroundColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Hexy.IsValidHexInput(ButtonBackgroundColorTextBox.Text))
                ExampleButton.Background = Hexy.HexToBrush(Hexy.CleanUpHex(ButtonBackgroundColorTextBox.Text));
        }

        private void ButtonForegroundColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Hexy.IsValidHexInput(ButtonForegroundColorTextBox.Text))
                ExampleButton.Foreground = Hexy.HexToBrush(Hexy.CleanUpHex(ButtonForegroundColorTextBox.Text));
        }

        private void TemplateNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ExampleButton.Content = TemplateNameTextBox.Text;
        }
    }
}
