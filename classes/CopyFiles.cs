using System.IO;

namespace MnM_UI.classes
{ 
    public class CopyFiles
    {
        private ParseJSON ParseJson { get; set; }

        public CopyFiles()
        {
            ParseJson = new();
        }

        public void CopyWindows(string templatePath, string gamePath)
        {
            string templateJson = File.ReadAllText(templatePath);
            string gameJson = "";

            if (File.Exists(gamePath))
            {
                gameJson = File.ReadAllText(gamePath);
            }

            ParseJSON.WindowList mergedData = ParseJson.MergeJson(ParseJson.Deserialize(templateJson), ParseJson.Deserialize(gameJson));
            string newJson = ParseJson.Serialize(mergedData);
        
            File.WriteAllText(gamePath, newJson);
        }

        public void CopyChats(string templatePath, string gamePath)
        {
            File.Copy(templatePath, gamePath, true);
        }
    }
}