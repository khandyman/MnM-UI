using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace MnM_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public required string TemplateDirectory { get; set; }
        public required string JournalDirectory { get; set; }

        public const string GameDirectory = @"C:\Users\%USERNAME%\AppData\LocalLow\Niche Worlds Cult\Monsters and Memories";

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists("config"))
            {
                string[] lines = File.ReadAllLines("config");
                
                foreach (string line in lines)
                {
                    if (line.StartsWith("Template: "))
                    {
                        string templatePath = line.Substring("Template: ".Length);
                        this.TemplateDirectory = templatePath;
                        txtTemplatePath.Text = templatePath;
                    }
                    else if (line.StartsWith("Journal: "))
                    {
                        string journalPath = line.Substring("Journal: ".Length);
                        this.JournalDirectory = journalPath;
                        txtJournalPath.Text = journalPath;
                    }
                }
            }
        }

        private void ExitCommand_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            BuildPaths buildPaths = new(this.TemplateDirectory, this.JournalDirectory);
            CopyFiles copyFiles = new();
            
            List<string> allCharacters = buildPaths.AllCharacters;
            string sourceWindowsPath = buildPaths.WindowsPath;
            string sourceChatsPath = buildPaths.ChatsPath;

            lstOutput.Items.Clear();

            foreach (string path in allCharacters)
            {
                string destWindowsPath = $@"{GameDirectory}\{path}\windows.json";
                string destChatsPath = $@"{GameDirectory}\{path}\chats.json";

                lstOutput.Items.Add($"Merging and copying windows.json to {path}...");
                copyFiles.CopyWindows(sourceWindowsPath, destWindowsPath);

                lstOutput.Items.Add($"Copying chats.json to {path}...");
                copyFiles.CopyChats(sourceChatsPath, destChatsPath);
               
                lstOutput.Items.Add("--------------------------------------------");
            }
        }

        private void ButtonMerge_Click(object sender, RoutedEventArgs e)
        {
            // do stuff
        }

        private void ChangeTemplate_Click(object sender, RoutedEventArgs e)
        {
            txtTemplatePath.Text = GetFolder();
            SaveConfig(txtTemplatePath.Text, "Template");
        }

        private void ChangeJournal_Click(object sender, RoutedEventArgs e)
        {
            txtJournalPath.Text = GetFolder();
            SaveConfig(txtJournalPath.Text, "Journal");
        }

        private string GetFolder()
        {
            var dialog = new OpenFolderDialog();
            dialog.Title = "Select Your Journal Backup Directory";

            if (dialog.ShowDialog() == true)
            {
                return dialog.FolderName;
            }

            return "";
        }

        private void SaveConfig(string path, string type)
        {
            if (File.Exists("config"))
            {
                string[] lines = File.ReadAllLines("config");

                var match = lines.FirstOrDefault(line => line.StartsWith($"{type}: "));

                if (match != null)
                {
                    int index = Array.IndexOf(lines, match);
                    lines[index] = $"{type}: " + path;
                }
                else
                {
                    lines = lines.Append($"{type}: " + path).ToArray();
                }

                File.WriteAllLines("config", lines);
            }
            else
            {
                File.WriteAllText("config", $"{type}: {path}");
            }
        }
    }
}