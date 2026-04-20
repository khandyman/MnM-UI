using System;
using System.Text.Json.Nodes;
using System.IO;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text.Json;

public class CopyFiles
{
	private string _sourcePath;
	private string _destPath;

    public CopyFiles(string sourcePath, string destPath)
    {
        _sourcePath = sourcePath;
        _destPath = destPath;
    }

    public string GetSourcePath()
    {
        return _sourcePath;
    }

    public string GetDestPath()
    {
        return _destPath;
    }

    public void SetSourcePath(string sourcePath)
    {
        _sourcePath = sourcePath;
    }

    public void SetDestPath(string destPath)
    {
        _destPath = destPath;
    }

    private static JsonNode JsonString(string path)
    {
        string jsonString = File.ReadAllText(path);

        JsonNode? node = JsonNode.Parse(jsonString);
        //jsonDict.Add("SaveData", JsonSerializer.Deserialize<OrderedDictionary>(jsonString));

        if (node != null)
        {
            return node;
        }
        else
        {
            return null;
        }
    }

    public void CopyWindows()
    {
        //OrderedDictionary jsonData = new();
        //OrderedDictionary saveData = new();

        string jsonString = File.ReadAllText(GetSourcePath());
        JsonNode? node = JsonNode.Parse(jsonString);
        //JsonNode jsonData = JsonString(GetSourcePath());
        //saveData = jsonData.Keys.ToString();

        var saveData = node.AsObject();

        foreach (var item in saveData)
        {
        //    foreach (KeyValuePair<string, JsonNode?> element in property.AsObject())
            Console.WriteLine(item);
        //        break;
        }
    }
}
