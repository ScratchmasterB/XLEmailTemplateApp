using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Path = System.IO.Path;

namespace XLEmailTemplateApp
{

    public partial class MainWindow : Window
    {
        TextBox NameTextBox = new TextBox();

        public Button rightClickedButton;
        public Button leftClickedButton;

        public MainWindow()
        {
            InitializeComponent();

            Options.LoadSettings();

            MyTemplates.isNewVersion = false;
            MyTemplates.CreateEmailTemplatesFolderPath();
            MySignature.CreateSignatureFolderPath();

            AddNameLabel();
            AddTemplateButtons();
            AddSignatureButton();

        }


        private void IanOption()
        {

        }

        private void ChangeButtonColor_OnClick(object sender, RoutedEventArgs e)
        {

            rightClickedButton.Background = Brushes.Red;

        }

        public void AddNameLabel()
        {
            if (Options.hasNameOptionOn)
            {
                var addresseeLabel = new Label
                {
                    Content = "Contact name:"
                };
                ButtonPanel.Children.Add(addresseeLabel);
                NameTextBox = new TextBox();
                ButtonPanel.Children.Add(NameTextBox);
            }
            else
            {
                var spacerLabel = new Label();
                ButtonPanel.Children.Add(spacerLabel);
            }
        }

        public void AddTemplateButtons()
        {
            var xmlFiles = new DirectoryInfo(MyTemplates.FolderPath).EnumerateFiles("*.xml").ToList();

            if (xmlFiles.Count == 0) CreateNewTemplateButton();
            else
            {
                foreach (var xmlFile in xmlFiles)
                {
                    var template = new Template(xmlFile.Name);

                    template.ReadXmlTemplate();
                    template.SetButtonName();

                    var button = new Button()
                    {
                        Name = Path.GetFileNameWithoutExtension(xmlFile.Name),
                        Content = template.TemplateName,
                        Background = Hexy.HexToBrush(Hexy.CleanUpHex(template.ButtonBackgroundColor)),
                        Foreground = Hexy.HexToBrush(Hexy.CleanUpHex(template.ButtonForegroundColor))
                    };

                    button.Click += new RoutedEventHandler(LoadTemplateToClipboard_Click);
                    button.MouseRightButtonDown += new MouseButtonEventHandler(TemplateButton_MouseRightButtonDown);
                    ButtonPanel.Children.Add(button);
                }
            }
        }

        private void CreateNewTemplateButton()
        {
            var button = new Button()
            {
                Name = "NewTemplateButton",
                Content = "New Template",
                Height = 30,
                Margin = new Thickness(5, 10, 5, 5)
            };
            button.Click += new RoutedEventHandler(NewTemplateButton_Click);
            ButtonPanel.Children.Add(button);
        }

        public void AddSignatureButton()
        {

            if (File.Exists(MySignature.FilePath))
            {
                MySignature.LoadFromFile();
            }
            else
            {
                MySignature.WriteToFile();
            }

            var button = new Button()
            {
                Name = "SignatureButton",
                Content = "Signature",
                Height = 60,
                Margin = new Thickness(5, 20, 5, 5),
                Background = Hexy.HexToBrush(Hexy.CleanUpHex(MySignature.ButtonBackground)),
                Foreground = Hexy.HexToBrush(Hexy.CleanUpHex(MySignature.ButtonForeground))
            };
            button.Click += new RoutedEventHandler(LoadSignatureToClipboard_Click);
            button.MouseRightButtonDown += new MouseButtonEventHandler(SignatureButton_MouseRightButtonDown);
            ButtonPanel.Children.Add(button);
        }

        private void SignatureButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            rightClickedButton = sender as Button;
            ContextMenu cm = this.FindResource("signatureButtonContextMenu") as ContextMenu;
            cm.PlacementTarget = rightClickedButton;
            cm.IsOpen = true;
        }

        private void LoadSignatureToClipboard_Click(object sender, RoutedEventArgs e)
        {
            MySignature.LoadFromFile();
            Clipboard.Clear();
            Clipboard.SetText(MySignature.Text);
            NameTextBox.Clear();
        }

        public void LoadTemplateToClipboard_Click(object sender, RoutedEventArgs e)
        {
            var fileName = (sender as Button).Name + ".xml";

            var template = new Template(fileName);

            template.ReadXmlTemplate();

            template.AlterGreetingForTimeOfDay();
            template.AlterClosingForDayOfWeek();
            template.InsertAddresseeIntoTemplate(NameTextBox.Text);

            var email = WriteEmail(template);

            Clipboard.Clear();
            Clipboard.SetText(email.ToString());

            NameTextBox.Clear();
        }



        private void TemplateButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            rightClickedButton = sender as Button;
            ContextMenu cm = this.FindResource("templateButtonContextMenu") as ContextMenu;
            cm.PlacementTarget = rightClickedButton;
            cm.IsOpen = true;
        }

        private void NewTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTemplate();
        }

        public void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            if (rightClickedButton.Name == "SignatureButton")
            {
                var newWindow = new EditSignatureWindow();
                OpenNewWindow(newWindow);
            }
            else
            {
                var newWindow = new EditTemplateWindow(rightClickedButton);
                OpenNewWindow(newWindow);
            }

        }

        public void CreateNewVersion_OnClick(object sender, RoutedEventArgs e)
        {
            MyTemplates.isNewVersion = true;
            var newWindow = new EditTemplateWindow(rightClickedButton);
            OpenNewWindow(newWindow);
        }

        public void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult deleteConfirmation = MessageBox.Show("Are you sure you want to delete this template?",
                "Delete Template", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (deleteConfirmation == MessageBoxResult.Yes)
            {
                DeleteTemplate();
            }
        }

        public void DeleteTemplate()
        {
            XLEmailTemplateApp.Template.Delete(rightClickedButton.Name + ".xml");
            var newWindow = new MainWindow();
            OpenNewWindow(newWindow);
        }


        private void CreateNewTemplate()
        {
            var newWindow = new CreateTemplateWindow();
            OpenNewWindow(newWindow);
        }

        public string WriteEmail(Template template)
        {
            StringBuilder email = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(template.Greeting))
            {
                email.Append(template.Greeting + " ");
                email.Append(template.Addressee);
                email.Append("\r\n\r\n");
            }

            email.Append(string.IsNullOrWhiteSpace(template.Body) ? "" : $"{template.Body}\r\n\r\n");
            email.Append(string.IsNullOrWhiteSpace(template.Closing) ? "" : $"{template.Closing}\r\n\r\n");
            email.Append(string.IsNullOrWhiteSpace(MySignature.Text) ? "" : MySignature.Text);

            return email.ToString();
        }

        ////////////////////////////
        ////////  MENU BAR  //////// 
        ////////////////////////////

        private void Help_HowToUse_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new HowToUseWindow();
            OpenNewWindow(newWindow);
        }

        private void Options_ResetAllButtonColors_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult colorResetConfirmation = MessageBox.Show("Are you sure you want to reset to default colors for ALL BUTTONS?",
                "Delete Template", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (colorResetConfirmation == MessageBoxResult.Yes)
            {
                ResetAllButtonColors();
            }
            var newWindow = new MainWindow();
            OpenNewWindow(newWindow);
        }

        private void ResetAllButtonColors()
        {
            var xmlFiles = new DirectoryInfo(MyTemplates.FolderPath).EnumerateFiles("*.xml").ToList();

            if (xmlFiles.Count != 0)
            {
                foreach (var xmlFile in xmlFiles)
                {
                    var template = new Template(xmlFile.Name);

                    template.ReadXmlTemplate();
                    template.ButtonBackgroundColor = "#FF000000";
                    template.ButtonForegroundColor = "#FFf5ce42";
                    template.WriteTemplateToFile();
                }
            }
            else
            {

            }

            MySignature.ButtonBackground = "#FF000000";
            MySignature.ButtonForeground = "#FFf5ce42";
            MySignature.WriteToFile();
        }

        private void Help_About_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AboutWindow();
            OpenNewWindow(newWindow);
        }

        private void File_New_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTemplate();
        }

        public void File_DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete ALL templates?", "Delete all templates", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                var xmlFiles = new DirectoryInfo(MyTemplates.FolderPath).EnumerateFiles("*.xml").ToList();
                foreach (var xmlFile in xmlFiles)
                {
                    XLEmailTemplateApp.Template.Delete(xmlFile.Name);
                }
            }

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

        private void File_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Options_Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow newWindow = new OptionsWindow();
            OpenNewWindow(newWindow);
        }
    }
}
