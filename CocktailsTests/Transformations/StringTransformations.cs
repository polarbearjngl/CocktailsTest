using TechTalk.SpecFlow;

namespace CocktailsTests.Transformations
{
    [Binding]
    public class StringTransformations
    {
        [StepArgumentTransformation]
        public List<string> TransformToListOfString(string commaSeparatedList)
        {
            return commaSeparatedList.Split(",").ToList();
        }
    }
}
