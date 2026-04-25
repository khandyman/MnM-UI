using System.IO;

namespace MnM_UI.classes
{
    public class BuildPaths
    {
        public string? WindowsPath { get; }
        public string? ChatsPath { get; }

        public BuildPaths(string? templatePath, string? journalPath)
        {
            WindowsPath = templatePath + @"\windows.json";
            ChatsPath = templatePath + @"\chats.json";
        }

        public List<string> GetCharacters(string gameDirectory)
        {
            string[] serverDirectories = Directory.GetDirectories(gameDirectory);
            List<string> allCharacters = new List<string>();

            foreach (string serverDirectory in serverDirectories)
            {
                string[] serverCharacters = Directory.GetDirectories(serverDirectory);

                foreach (string character in serverCharacters)
                {
                    string charName = new DirectoryInfo(character).Name;
                    string serverName = new DirectoryInfo(character).Parent?.Name ?? string.Empty;

                    allCharacters.Add($@"{serverName}\{charName}");
                }
            }

            return allCharacters;
        }
    }
}
