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

        public void CopyWindows(string sourcePath, string destPath)
        {
            string sourceJson = File.ReadAllText(sourcePath);
            string destJson = "";

            if (File.Exists(destPath))
            {
                destJson = File.ReadAllText(destPath);
            }

            ParseJSON.WindowList mergedData = ParseJson.MergeJson(ParseJson.Deserialize(sourceJson), ParseJson.Deserialize(destJson));
            string newJson = ParseJson.Serialize(mergedData);
        
            File.WriteAllText(destPath, newJson);
        }

        public void CopyChats(string sourcePath, string destPath)
        {
            File.Copy(sourcePath, destPath, true);
        }
    }
}