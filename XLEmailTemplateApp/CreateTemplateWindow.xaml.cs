using System;
using System.Windows;
using System.Windows.Controls;

namespace XLEmailTemplateApp
{

    public partial class CreateTemplateWindow : Window
    {
        public bool DefaultTextOptionOn { get; set; } = true;

        public CreateTemplateWindow()
        {
            InitializeComponent();
            if (DefaultTextOptionOn) SetDefaultText();
        }

        public void SetDefaultText()
        {
            TemplateNameTextBox.Text = "";
            GreetingTextBox.Text = "Good morning,";
            AddresseeTextBox.Text = "XXXX,";
            BodyTextBox.Text = "We recommend adding XXXX for any text that is yet to be determined, " +
                "so you can double-click and quickly add the text after pasting your email into AutoTask.  " +
                "If this default text ever gets annoying, don't worry, you will be able to turn this " +
                "feature off in Options soon.";
            ClosingTextBox.Text = "Thank you, XXXX, and have a wonderful rest of your Wednesday!";

            SignatureTextBox.Text = MySignature.Text;
            ButtonBackgroundColorTextBox.Text = "#000000";
            ButtonForegroundColorTextBox.Text = "#f5ce42";


        }

        private void SaveAndNewButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TemplateNameTextBox.Text))
            {
                MessageBox.Show("Template must have a name", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Char.IsNumber(TemplateNameTextBox.Text[0]))
            {
                MessageBox.Show("Template name cannot start with a number", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SaveTemplateToFile();
                TemplateNameTextBox.Clear();
                BodyTextBox.Clear();
            }
        }

        private void SaveAndCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TemplateNameTextBox.Text))
            {
                MessageBox.Show("Template must have a name", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Char.IsNumber(TemplateNameTextBox.Text[0]))
            {
                MessageBox.Show("Template name cannot start with a number", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (SaveTemplateToFile())
                {
                    var newWindow = new MainWindow();
                    newWindow.Show();
                    Close();
                }


            }

        }

        private bool SaveTemplateToFile()
        {
            if (!Hexy.IsValidHexInput(ButtonBackgroundColorTextBox.Text) || !Hexy.IsValidHexInput(ButtonForegroundColorTextBox.Text))
            {
                return false;
            }
            var template = new Template()
            {
                TemplateName = TemplateNameTextBox.Text,
                Greeting = GreetingTextBox.Text,
                Addressee = AddresseeTextBox.Text,
                Body = BodyTextBox.Text,
                Closing = ClosingTextBox.Text,
                ButtonBackgroundColor = ButtonBackgroundColorTextBox.Text,
                ButtonForegroundColor = ButtonForegroundColorTextBox.Text
            };

            template.SetTemplateFilePath();

            if (template.CheckIfFileExists())
            {
                MessageBoxResult result = MessageBox.Show("This will overwrite the previous template with this name.  Are you sure?", "File Overwrite",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No || result == MessageBoxResult.Cancel)
                    return false;
            }

            if (SignatureTextBox.Text != MySignature.Text)
            {
                // UpdateSignature()
            }


            if (template.IsFilledOutProperly()) template.WriteTemplateToFile();
            return true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow();
            newWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            newWindow.Left = this.Left;
            newWindow.Top = this.Top;
            newWindow.Show();
            Close();
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
