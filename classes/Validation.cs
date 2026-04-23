using System.IO;
using System.Windows;

namespace MnM_UI.classes
{
    public class Validation
    {
        public Validation()
        {
        }

        public Boolean ValidateTemplateChange(string templatePath)
        {
            if (templatePath == string.Empty)
            {
                MessageBox.Show("Please select a valid Template directory before copying.",
                                "Missing Template Directory",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Directory.Exists(templatePath))
            {
                MessageBox.Show("The selected Template directory does not exist.",
                                "Invalid Directory Selected",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!File.Exists(templatePath + @"\windows.json") || !File.Exists(templatePath + @"\chats.json"))
            {
                MessageBox.Show("The selected Template directory does not contain the required windows.json and chats.json files.",
                                "Missing Required JSON Files",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public Boolean ValidateJournalChange(string journalPath)
        {
            if (journalPath == string.Empty)
            {
                MessageBox.Show("Please select a valid Journal directory before copying.",
                                "Missing Journal Directory",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Directory.Exists(journalPath))
            {
                MessageBox.Show("The selected Journal directory does not exist.",
                                "Invalid Directory Selected",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!File.Exists(journalPath + @"\Player.Log"))
            {
                MessageBox.Show("The selected Journal directory is not a valid Monsters and Memories directory.\n\n" +
                                "Please ensure it contains the required Player.Log file and server folders.",
                                "Incorrect Monsters and Memories Directory",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
