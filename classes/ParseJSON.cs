using Newtonsoft.Json;
using System.IO;

namespace MnM_UI.classes
{
    public class ParseJSON
    {
        public ParseJSON()
        {
        }

        public OwnerList GetOwnerList(string jsonFile)
        {
            if (string.IsNullOrEmpty(jsonFile))
            {
                return new OwnerList { OwnerData = new List<OwnerID>() };
            }
            else
            {
                OwnerList newData = JsonConvert.DeserializeObject<OwnerList>(jsonFile) ?? new OwnerList { OwnerData = new List<OwnerID>() };

                return newData;
            }
        }

        public string FindOwnerID(string ownerName)
        {
            string ownerID = string.Empty;
            if (!string.IsNullOrEmpty(ownerName))
            {
                string path = @"G:\My Drive\MnM\UI_Master\ownerIDs.json";
                string data = File.ReadAllText(path);

                OwnerList ownerList = GetOwnerList(data);

                foreach (var owner in ownerList.OwnerData)
                {
                    if (owner.name.Equals(ownerName, StringComparison.OrdinalIgnoreCase))
                    {
                        return owner.id;
                    }
                }
            }
            return ownerID;
        }

        public WindowList Deserialize(string jsonFile)
        {
            if (string.IsNullOrEmpty(jsonFile))
            {
                return new WindowList { SaveData = new List<WindowItem>() };
            }
            else
            {
                WindowList newData = JsonConvert.DeserializeObject<WindowList>(jsonFile) ?? new WindowList { SaveData = new List<WindowItem>() };

                return newData;
            }
        }

        public string Serialize(WindowList newData)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            string newJson = JsonConvert.SerializeObject(newData, settings);

            return newJson;
        }

        public WindowList MergeJson(WindowList templateData, WindowList gameData)
        {
            foreach (var templateItem in templateData.SaveData.ToList())
            {
                if (templateItem.identifier.Contains("bagpanel") && !templateItem.identifier.Contains("6588"))
                {
                    templateData.SaveData.Remove(templateItem);
                }
            }

            foreach (var gameItem in gameData.SaveData)
            {
                if (gameItem.identifier.Contains("bagpanel") && !gameItem.identifier.Contains("6588"))
                {
                    templateData.SaveData.Add(gameItem);
                }
            }

            return templateData;
        }

        public class OwnerList
        {
            public required List<OwnerID> OwnerData { get; set; }
        }

        public class OwnerID 
        {
            public required string name { get; set; }
            public required string id { get; set; }
        }

        public class WindowList
        {
            public required List<WindowItem> SaveData { get; set; }
        }

        public class WindowItem
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
}
