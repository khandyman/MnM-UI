using Newtonsoft.Json;

namespace MnM_UI.classes
{
    public class ParseJSON
    {
        public ParseJSON()
        {
        }

        public WindowList Deserialize(string sourceJson)
        {
            if (string.IsNullOrEmpty(sourceJson))
            {
                return new WindowList { SaveData = new List<WindowItem>() };
            }
            else
            {
                WindowList newData = JsonConvert.DeserializeObject<WindowList>(sourceJson);

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

        public WindowList MergeJson(WindowList sourceData, WindowList destData)
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
