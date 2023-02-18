using CocktailsTests.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace CocktailsTests.StepDefinitions
{
    [Binding]
    internal class CocktailsSteps
    {
        private readonly ScenarioContext scenarioContext;

        public CocktailsSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }
        
        [Then(@"the Cocktail '(.*response.*)' is not empty and contains '(\d*)' item")]
        public void CheckCocktailResponseIsNotEmpty(string responseName, int itemCount)
        {
            RestResponse response = scenarioContext.Get<RestResponse>(responseName);
            Assert.Multiple(() =>
            {
                Assert.That(response.Content, Is.Not.Null, $"Content of '{responseName}' should be not Null");
                Assert.That(response.Content, Is.Not.Empty, $"Content of '{responseName}' should be not Empty");
            });

            DrinksList? drinksList = JsonConvert.DeserializeObject<DrinksList>(response.Content);
            Assert.That(drinksList.Drinks, Is.Not.Null, $"`Drinks` list of '{responseName}' should be not Null");
            Assert.That(drinksList.Drinks.Count, Is.EqualTo(itemCount), $"Count of items in '{responseName}' should be {itemCount}");
        }

        [Then(@"the Cocktail '(.*response.*)' is empty")]
        public void CheckCocktailResponseIsEmpty(string responseName)
        {
            RestResponse response = scenarioContext.Get<RestResponse>(responseName);
            Assert.Multiple(() =>
            {
                Assert.That(response.Content, Is.Not.Null, $"Content of '{responseName}' should be not Null");
                Assert.That(response.Content, Is.Not.Empty, $"Content of '{responseName}' should be not Empty");
            });

            DrinksList? drinksList = JsonConvert.DeserializeObject<DrinksList>(response.Content);
            Assert.That(drinksList.Drinks, Is.Null, $"`Drinks` list of '{responseName}' should be Null");
        }

        [Then("'(.*)' are same")]
        public void CocktailsResponsesAreSame(List<string> cocktailsResponsesNames)
        {
            List<string> drinksListContent = new List<string>();
            foreach(string responseName in cocktailsResponsesNames)
            {
                RestResponse response = scenarioContext.Get<RestResponse>(responseName);
                drinksListContent.Add(response.Content);
            }
            Assert.That(drinksListContent.Distinct().Count(), Is.EqualTo(1), $"Responses content for '{cocktailsResponsesNames}' should have same content");
        }   
    }
}
