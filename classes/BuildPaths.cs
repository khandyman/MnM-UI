using System;
using System.IO;
using System.Windows.Shapes;

public class BuildPaths
{
	public string WindowsPath { get; }
    public string ChatsPath { get; }
    public List<string> AllCharacters { get; }

    public BuildPaths(string templatePath, string journalPath)
	{
		WindowsPath = templatePath + @"\windows.json";
        ChatsPath = templatePath + @"\chats.json";
        AllCharacters = GetCharacters();
	}

	public List<string> GetCharacters()
	{
        string gameDirectory = $@"C:\Users\{Environment.UserName}\AppData\LocalLow\Niche Worlds Cult\Monsters and Memories";
        string[] serverDirectories = Directory.GetDirectories(gameDirectory);
        List<string> allCharacters = new List<string>();

        foreach (string serverDirectory in serverDirectories)
        {
            string[] serverCharacters = Directory.GetDirectories(serverDirectory);

            foreach (string character in serverCharacters)
            {
                string charName = new DirectoryInfo(character).Name;
                string serverName = new DirectoryInfo(character).Parent.Name;

                allCharacters.Add($@"{serverName}\{charName}");
            }
        }

        return allCharacters;
    }
}
