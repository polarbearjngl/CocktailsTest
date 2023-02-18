using Newtonsoft.Json;

namespace CocktailsTests.Models
{
    public class IngredientList
    {
        [JsonProperty("ingredients")]
        public List<Ingredient>? Ingredients { get; set; }
    }
}
