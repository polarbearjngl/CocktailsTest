using CocktailsTests.Framework.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Schema;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;

namespace CocktailsTests.StepDefinitions
{
    [Binding]
    public class CommonSteps
    {
        private readonly ScenarioContext scenarioContext;

        public CommonSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then(@"the status code of the '(.*response.*)' is '(\d*)'")]
        public void StatusCodeOfResponseIs(string responseName, int statusCode)
        {
            RestResponse resp = scenarioContext.Get<RestResponse>(responseName);
            Assert.That((int)resp.StatusCode, Is.EqualTo(statusCode), "Status code should match to expected");
        }

        [Then(@"the '(.*response.*)' matches json schema '(.*)'")]
        public static void AssertResponseSchemaIsValid(RestResponse response, string schemaName)
        {
            var schemaPath = Path.Combine(AppContext.BaseDirectory, "JsonSchemas", $"{schemaName}.json");
            using (StreamReader file = File.OpenText(schemaPath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JSchema schema = JSchema.Load(reader, new JSchemaReaderSettings
                {
                    Resolver = new JSchemaUrlResolver(),
                    BaseUri = new Uri(schemaPath)
                });
                AttachmentHelper.AddAttachmentAsJson("json schema", schema);
                var model = JObject.Parse(response.Content);
                IList<string> messages;
                Assert.That(model.IsValid(schema, out messages), string.Join("\r\n", messages.ToArray()));
            }
        }
    }
}
