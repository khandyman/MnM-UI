using MnM_UI.classes;
using System.Windows;
using System.Windows.Input;

namespace MnM_UI
{
    /// <summary>
    /// Interaction logic for SelectWindow.xaml
    /// </summary>
    public partial class SelectWindow : Window
    {
        MainWindow Parent;

        public SelectWindow(MainWindow parent)
        {
            InitializeComponent();

            Parent = parent;

            BuildPaths buildPaths = new BuildPaths(MainWindow.GameDirectory, MainWindow.GameDirectory);
            string[] characters = buildPaths.GetCharacters(MainWindow.GameDirectory).ToArray();
            cmbCharacters.ItemsSource = characters;
        }

        private void SelectCommand_Click(object sender, RoutedEventArgs e)
        {
            string selectedCharacter = cmbCharacters.SelectedItem.ToString();
            string templatePath = $@"{MainWindow.GameDirectory}\{selectedCharacter}";
            Parent.txtTemplatePath.Text = templatePath;
            Parent.chkCopyCharacter.IsChecked = false;
            Parent.SaveConfig(templatePath, "Template");
            this.Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
