using RestSharp;

namespace CocktailsTests.Framework.Api
{
    public class CocktailsApi
    {

        public RestRequest SearchByIngredientName(string name)
        {
            string endpoint = Configuration.Configuration.ApiUrl + Configuration.Configuration.SearchEndpoint;
            RestRequest request = new RestRequest(endpoint, Method.Get).AddParameter("i", name);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            return request;
        }

        public RestRequest SearchByCocktailsName(string ingridient)
        {
            string endpoint = Configuration.Configuration.ApiUrl + Configuration.Configuration.SearchEndpoint;
            RestRequest request = new RestRequest(endpoint, Method.Get).AddParameter("s", ingridient);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            return request;
        }
    }

}
