using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using Newtonsoft.Json;

public class ParseJSON
{
    //public SaveRoot SourceData { get; set; }
    //public SaveRoot DestData { get; set; }

    public ParseJSON()
    {
        //SourceData = Deserialize(sourceJson);
        //DestData = Deserialize(destJson);
    }

    public SaveRoot Deserialize(string sourceJson)
    {
        if (string.IsNullOrEmpty(sourceJson))
        {
            return new SaveRoot { SaveData = new List<SaveItem>() };
        }
        else
        {
            SaveRoot newData = JsonConvert.DeserializeObject<SaveRoot>(sourceJson);

            return newData;
        }
    }

    public string Serialize(SaveRoot newData)
    {
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        string newJson = JsonConvert.SerializeObject(newData, settings);

        return newJson;
    }

    public SaveRoot MergeJson(SaveRoot sourceData, SaveRoot destData)
    {
        foreach (var sourceItem in sourceData.SaveData.ToList())
        {
            if (sourceItem.identifier.Contains("bagpanel") && !sourceItem.identifier.Contains("6588"))
            {
                sourceData.SaveData.Remove(sourceItem);
            }
        }

        foreach (var destItem in destData.SaveData)
        {
            if (destItem.identifier.Contains("bagpanel") && !destItem.identifier.Contains("6588"))
            {
                sourceData.SaveData.Add(destItem);
            }
        }

        return sourceData;
    }

    public class SaveRoot
    {
        public required List<SaveItem> SaveData { get; set; }
    }

    public class SaveItem
    {
        public required string identifier { get; set; }
        public required VectorData position { get; set; }
        public required VectorData anchorMin { get; set; }
        public required VectorData anchorMax { get; set; }
        public required VectorData pivot { get; set; }
        public required VectorData sizeDelta { get; set; }
        public required bool locked { get; set; }
        public required bool dimensionsInverted { get; set; }
        public required bool saveSize { get; set; }
        public required decimal windowScale { get; set; }
        public required bool isCollapsed { get; set; }
    }

    public class VectorData
    {
        public required decimal x { get; set; }
        public required decimal y { get; set; }
        public decimal? z { get; set; }
        public required decimal magnitude { get; set; }
        public required decimal sqrMagnitude { get; set; }
        public VectorData? normalized { get; set; }
    }
}
