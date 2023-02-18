using CocktailsTests.Framework.Helpers;

namespace CocktailsTests.Framework.Configuration
{
    internal class Configuration
    {
        private static JsonDataReader appConfig = new JsonDataReader(Path.Combine(AppContext.BaseDirectory, "Config", $"AppConfig.json"));
        

        public static string StartUrl => appConfig.GetString("startUrl");
        public static string ApiUrl => appConfig.GetString("apiUrl");
        public static string SearchEndpoint => appConfig.GetString("searchEndpoint");
    }
}
