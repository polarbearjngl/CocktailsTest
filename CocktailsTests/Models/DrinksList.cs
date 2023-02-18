using Newtonsoft.Json;

namespace CocktailsTests.Models
{
    internal class DrinksList
    {
        [JsonProperty("drinks")]
        public List<Drink>? Drinks { get; set; }
    }
}
