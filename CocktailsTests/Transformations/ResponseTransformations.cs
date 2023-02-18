using RestSharp;
using TechTalk.SpecFlow;

namespace CocktailsTests.Transformations
{
    [Binding]
    internal class ResponseTransformations
    {
        private readonly ScenarioContext scenarioContext;

        public ResponseTransformations(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }
        
        [StepArgumentTransformation("(.*response.*)")]
        public RestResponse Response(string key)
        {
            return scenarioContext.ContainsKey(key)
                ? scenarioContext.Get<RestResponse>(key)
                : throw new ArgumentException($"No response is stored in the context with name [{key}]");
        }
    }
}
