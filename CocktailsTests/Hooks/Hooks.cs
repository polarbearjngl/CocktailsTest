using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist.ValueRetrievers;
using TechTalk.SpecFlow.Assist;

namespace CocktailsTests.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext context;

        public Hooks(ScenarioContext context)
        {
            this.context = context;
        }

        [BeforeScenario]
        public static void BeforeTestRun()
        {
            Service.Instance.ValueRetrievers.Register(new NullValueRetriever("<null>"));
        }
    }
}
