using CocktailsTests.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CocktailsTests.Transformations
{
    [Binding]
    public class TableTransformations
    {
        [StepArgumentTransformation]
        public Ingredient TransformModel(Table inputTable) => inputTable.CreateInstance<Ingredient>();
    }
}
