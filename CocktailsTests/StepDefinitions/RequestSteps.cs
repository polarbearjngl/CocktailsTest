using CocktailsTests.Framework.Helpers;
using CocktailsTests.Framework.Api;
using TechTalk.SpecFlow;


namespace CocktailsTests.StepDefinitions
{
    [Binding]
    public class RequestSteps
    {
        readonly RequestHandler requestHandler;
        readonly ScenarioContext scenarioContext;
        CocktailsApi cocktailsApi;

        public RequestSteps(RequestHandler requestHandler, ScenarioContext scenarioContext, CocktailsApi cocktailsApi)
        {
            this.requestHandler = requestHandler;
            this.scenarioContext = scenarioContext;
            this.cocktailsApi = cocktailsApi;
        }

        [When(@"I Get ingredients by Name '(\w*)' and saving the '(.*response.*)'")]
        public void GetIngredientsByName(string name, string contextKey)
        {
            var response = requestHandler.Execute(cocktailsApi.SearchByIngredientName(name));
            scenarioContext.Add(contextKey, response);
        }

        [When(@"I Get Cocktails by Name '([\w'%]+)' and saving the '(.*response.*)'")]
        public void GetCocktailsByName(string name, string contextKey)
        {
            var response = requestHandler.Execute(cocktailsApi.SearchByCocktailsName(name));
            scenarioContext.Add(contextKey, response);
        }

    }
}
