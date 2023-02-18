using Newtonsoft.Json;


namespace CocktailsTests.Framework.Helpers
{
    public class JsonDataReader
    {
        private Dictionary<string, object> jsonData;


        public JsonDataReader(string filePath)
        {
            jsonData = new Dictionary<string, object>();
            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                jsonData = (Dictionary<string, object>)serializer.Deserialize(file, typeof(Dictionary<string, object>));
            }
        }

        public string GetString(string key)
        {
            if (jsonData.ContainsKey(key))
            {
                return (string)jsonData[key];
            }
            return "";
        }

    }
}
