using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using CocktailsTests.Models;
using CocktailsTests.Enums;

namespace CocktailsTests.StepDefinitions
{
    [Binding]
    public class IngredientsSteps
    {
        private readonly ScenarioContext scenarioContext;

        public IngredientsSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then(@"the Ingredient '(.*response.*)' is not empty and contains '(\d*)' item")]
        public void CheckIngredientResponseIsNotEmpty(string responseName, int itemCount)
        {
            RestResponse response = scenarioContext.Get<RestResponse>(responseName);
            Assert.Multiple(() =>
            {
                Assert.That(response.Content, Is.Not.Null, $"Content of '{responseName}' should be not Null");
                Assert.That(response.Content, Is.Not.Empty, $"Content of '{responseName}' should be not Empty");
            });
            
            IngredientList? ingredients = JsonConvert.DeserializeObject<IngredientList>(response.Content);
            Assert.That(ingredients.Ingredients, Is.Not.Null, $"`Ingredients` list of '{responseName}' should be not Null");
            Assert.That(ingredients.Ingredients.Count, Is.EqualTo(itemCount), $"Count '{responseName}' should be equal to {itemCount}");
        }

        [Then(@"the Ingredient '(.*response.*)' is empty")]
        public void CheckIngredientResponseIsEmpty(string responseName)
        {
            RestResponse response = scenarioContext.Get<RestResponse>(responseName);
            Assert.Multiple(() =>
            {
                Assert.That(response.Content, Is.Not.Null, $"Content of '{responseName}' should be not Null");
                Assert.That(response.Content, Is.Not.Empty, $"Content of '{responseName}' should be not Empty");
            });

            IngredientList? ingredients = JsonConvert.DeserializeObject<IngredientList>(response.Content);
            Assert.That(ingredients.Ingredients, Is.Null, $"`Ingredients` list of '{responseName}' should be not Null");
        }

        [Then(@"the '(Alcoholic|NonAlcoholic)' Ingredient '(.*response.*)' content for '(\w*)' is valid")]
        public void CheckIngredientResponseContentStruct(IngredientTypes ingredientType, string responseName, string name)
        {
            RestResponse response = scenarioContext.Get<RestResponse>(responseName);

            IngredientList? ingredients = JsonConvert.DeserializeObject<IngredientList>(response.Content);
            Ingredient ingredient = ingredients.Ingredients.Single(ingredient => ingredient.IngredientName == name);

            Assert.That(ingredient, Is.Not.Null, $"Ingredient '{name}' should be not Null in '{responseName}'");
            Assert.Multiple(() =>
            {
                Assert.That(ingredient.IngredientId, Is.Not.Null, $"Field `idIngredient` should be not Null");
                Assert.That(ingredient.Description, Is.Not.Null, $"Field `strDescription` should be not Null");
                Assert.That(ingredient.Type, Is.Not.Null, $"Field `strType` should be not Null");

                string alco = (ingredientType == IngredientTypes.Alcoholic) ? Constants.Common.Alcoholic : Constants.Common.NonAlcoholic;
                if (ingredientType == IngredientTypes.Alcoholic)
                {
                    Assert.That(ingredient.Alcohol, Is.EqualTo(alco), $"Field `strAlcohol` should be '{alco}' for '{ingredientType}'");
                    Assert.That(ingredient.ABV, Is.Not.Null, $"Field `strABV` should be not Null for '{ingredientType}'");
                }
                else
                {
                    Assert.That(ingredient.Alcohol, Is.EqualTo(alco), $"Field `strAlcohol` should be '{alco}' for '{ingredientType}'");
                    Assert.That(ingredient.ABV, Is.Null, $"Field `strABV` should be Null for '{ingredientType}'");
                }
            });
        }

        [Then(@"the data from '(.*response.*)' for '(\w*)' saved into '(\w*)' for validation")]
        public void DataFromIngredientsResponseSavedForValidation(string responseName, string name, string ingredient)
        {
            RestResponse response = scenarioContext.Get<RestResponse>(responseName);
            IngredientList? ingredients = JsonConvert.DeserializeObject<IngredientList>(response.Content);
            Ingredient ingredientForValidation = ingredients.Ingredients.Single(ingredient => ingredient.IngredientName == name);
            scenarioContext.Set(ingredientForValidation, ingredient);
        }

        [Then(@"the Ingredient '(\w*)' equal to expected:")]
        public void ValidateActualIngredient(string ingredient, Ingredient expected)
        {
            Ingredient actual = scenarioContext.Get<Ingredient>(ingredient);
            Assert.Multiple(() =>
            {
                Assert.That(actual.IngredientId, Is.EqualTo(expected.IngredientId));
                Assert.That(actual.IngredientName, Is.EqualTo(expected.IngredientName));
                Assert.That(actual.Type, Is.EqualTo(expected.Type));
                Assert.That(actual.Alcohol, Is.EqualTo(expected.Alcohol));
                Assert.That(actual.ABV, Is.EqualTo(expected.ABV));
            });
        }

    }
}
