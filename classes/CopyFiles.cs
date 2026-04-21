using MnM_UI;
using System;
using System.IO;

public class CopyFiles
{
	//private string SourceDir { get; set; }
 //   private string SourceWindowsJson { get; set; }
 //   private string SourceChatsJson { get; set; }
 //   private string? DestDir { get; set; }
 //   private string[]? DestCharDirs { get; set; }
 //   private string? DestJson { get; set; }
    private ParseJSON ParseJson { get; set; }

    public CopyFiles()
    {
        ParseJson = new();
    }

    //public void WriteToFile(string destPath, string destJson)
    //{
    //    File.WriteAllText(destPath, destJson);
    //}

    public void CopyWindows(string sourcePath, string destPath)
    {
        string sourceJson = File.ReadAllText(sourcePath);
        string destJson = "";

        if (File.Exists(destPath))
        {
            destJson = File.ReadAllText(destPath);
        }

        ParseJSON.SaveRoot mergedData = ParseJson.MergeJson(ParseJson.Deserialize(sourceJson), ParseJson.Deserialize(destJson));
        string newJson = ParseJson.Serialize(mergedData);
        
        File.WriteAllText(destPath, newJson);
    }

    public void CopyChats(string sourcePath, string destPath)
    {
        File.Copy(sourcePath, destPath, true);
    }
}
