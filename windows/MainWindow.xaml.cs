using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MnM_UI.classes;

namespace MnM_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public required string TemplateDirectory { get; set; }
        public required string JournalDirectory { get; set; }
        public required MergeJournals MergeJournals { get; set; }
        public required CopyFiles CopyFiles { get; set; }
        public required Validation Validation { get; set; }

        public static readonly string GameDirectory = $@"C:\Users\{Environment.UserName}\AppData\LocalLow\Niche Worlds Cult\Monsters and Memories";

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

            MergeJournals = new();
            CopyFiles = new();
            Validation = new();
        }

        private void HelpCommand_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Owner = this;
            helpWindow.ShowDialog();
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
            if (Validation.ValidateTemplateChange(txtTemplatePath.Text))
            {
                BuildPaths buildPaths = new(this.TemplateDirectory, this.JournalDirectory);

                List<string> allCharacters = buildPaths.GetCharacters(GameDirectory);
                string sourceWindowsPath = buildPaths.WindowsPath;
                string sourceChatsPath = buildPaths.ChatsPath;

                lstOutput.Items.Clear();

                foreach (string path in allCharacters)
                {
                    string destWindowsPath = $@"{GameDirectory}\{path}\windows.json";
                    string destChatsPath = $@"{GameDirectory}\{path}\chats.json";

                    lstOutput.Items.Add($"Merging and copying windows.json to {path}...");
                    CopyFiles.CopyWindows(sourceWindowsPath, destWindowsPath);

                    lstOutput.Items.Add($"Copying chats.json to {path}...");
                    CopyFiles.CopyChats(sourceChatsPath, destChatsPath);

                    lstOutput.Items.Add("--------------------------------------------");
                }
            }
        }

        private void ButtonMerge_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.ValidateJournalChange(txtJournalPath.Text))
            {
                BuildPaths buildPaths = new(this.TemplateDirectory, this.JournalDirectory);

                List<string> allCharacters = buildPaths.GetCharacters(GameDirectory);

                foreach (string character in allCharacters)
                {
                    string sourceJournalPath = $@"{GameDirectory}\{character}\journal";
                    string destJournalPath = $@"{JournalDirectory}\{character}\journal";

                    if (Directory.Exists(sourceJournalPath))
                    {
                        string[] sourceJournals = Directory.GetFiles(sourceJournalPath).ToArray();

                        foreach (string sourceJournal in sourceJournals)
                        {
                            string journalFile = Path.GetFileName(sourceJournal);
                            lstOutput.Items.Add($"Merging {journalFile} for {character}...");

                            string destJournal = $@"{destJournalPath}\{journalFile}";
                            MergeJournals.CopyJournal(sourceJournal, destJournal);
                        }
                    }

                    if (Directory.Exists(destJournalPath))
                    {
                        string[] destJournals = Directory.GetFiles(destJournalPath).ToArray();

                        foreach (string destJournal in destJournals)
                        {
                            string journalFile = Path.GetFileName(destJournal);
                            string sourceJournal = $@"{sourceJournalPath}\{journalFile}";
                            MergeJournals.CopyJournal(destJournal, sourceJournal);
                        }
                    }
                }
            }
        }

        private void ChangeTemplate_Click(object sender, RoutedEventArgs e)
        {
            txtTemplatePath.Text = GetFolder("template");
            SaveConfig(txtTemplatePath.Text, "Template");
        }

        private void ChangeJournal_Click(object sender, RoutedEventArgs e)
        {
            txtJournalPath.Text = GetFolder("journal");
            SaveConfig(txtJournalPath.Text, "Journal");
        }

        private string GetFolder(string type)
        {
            var dialog = new OpenFolderDialog();

            if (type == "template")
            {
                dialog.Title = "Select a \"Template\" Directory with both windows.json and chats.json inside";
            }
            else if (type == "journal")
            {
                dialog.Title = "Select a Monsters and Memories directory you want to merge journals to/from";
            }

            if (dialog.ShowDialog() == true)
            {
                return dialog.FolderName;
            }

            return "";
        }

        public void SaveConfig(string path, string type)
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

        private void CopyCharacter_Click(object sender, RoutedEventArgs e)
        {
            SelectWindow selectWindow = new SelectWindow(this);
            selectWindow.Owner = this;
            selectWindow.ShowDialog();
        }

        private void NewBackup_Click(object sender, RoutedEventArgs e)
        {
            string backupPath;
            var dialog = new OpenFolderDialog();
            dialog.Title = "Select a location for your backup directory";

            if (dialog.ShowDialog() == true)
            {
                if (System.IO.Path.GetFileName(dialog.FolderName) != "Monsters and Memories")
                {
                    backupPath = $@"{dialog.FolderName}\Monsters and Memories";
                }
                else
                {
                    backupPath = dialog.FolderName;
                }

                string gamePath = $@"C:\Users\{Environment.UserName}\AppData\LocalLow\Niche Worlds Cult\Monsters and Memories";
                
                //Directory.CreateDirectory(backupPath);
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(gamePath, backupPath, true);
                SaveConfig(backupPath, "Journal");
                txtJournalPath.Text = backupPath; 
            }

            chkNewBackup.IsChecked = false;
        }
    }
}