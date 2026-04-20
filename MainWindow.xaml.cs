using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace MnM_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ParseJSON parseJSON = new();
            MergeJournals mergeJournals = new();
            

            
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
            string sourcePath = "G:\\My Drive\\MnM\\UI_Master\\windows.json";
            CopyFiles copyFiles = new(sourcePath,"");
            copyFiles.CopyWindows();

        }

        private void ButtonMerge_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}