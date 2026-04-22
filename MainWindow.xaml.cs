//using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
using Microsoft.Win32;


namespace MnM_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string SourceDir { get; set; }
        public string DestDir { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SourceDir = $"G:\\My Drive\\MnM\\UI_Master\\";
            DestDir = $"C:\\Users\\{Environment.UserName}\\AppData\\LocalLow\\Niche Worlds Cult\\Monsters and Memories\\beta1\\";

            this.txtSourcePath.Text = SourceDir;
        }

        private void SetCommand_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            dialog.Title = "Select Your Master JSON File Directory";

            if (dialog.ShowDialog() == true)
            {
                txtSourcePath.Text = dialog.FolderName;
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
            //var mainWindow = Application.Current.MainWindow as MainWindow;

            string sourceDir = this.SourceDir;
            string sourceWindowsPath = sourceDir + "windows.json";
            string sourceChatsPath = sourceDir + "chats.json";

            string destDir = this.DestDir;
            string[] charDirs = Directory.GetDirectories(destDir);

            CopyFiles copyFiles = new();

            lstOutput.Items.Clear();

            foreach (string path in charDirs)
            {
                string destWindowsPath = path + "\\windows.json";
                string destChatsPath = path + "\\chats.json";
                string charName = new DirectoryInfo(path).Name;

                lstOutput.Items.Add($"Merging and copying windows.json to {charName}...");
                copyFiles.CopyWindows(sourceWindowsPath, destWindowsPath);

                lstOutput.Items.Add($"Copying chats.json to {charName}...");
                copyFiles.CopyChats(sourceChatsPath, destChatsPath);
               
                lstOutput.Items.Add("--------------------------------------------");
            }
        }

        private void ButtonMerge_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}