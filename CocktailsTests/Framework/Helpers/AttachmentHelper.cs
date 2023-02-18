using Allure.Commons;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Text;

namespace CocktailsTests.Framework.Helpers
{
    internal class AttachmentHelper
    {
        private const string JsonType = "application/json";

        public static void AddAttachment(string pathToFile, string name = null)
        {
            TestContext.AddTestAttachment(pathToFile);
            AllureLifecycle.Instance.AddAttachment(pathToFile, name);
        }

        public static void AddAttachment(string name, string type, string content, string fileExtension = "")
        {
            var utfBytes = Encoding.UTF8.GetBytes(content);
            AllureLifecycle.Instance.AddAttachment(name, type, utfBytes, fileExtension);
            var filePath = $"{name}_{DateTime.Now:yyyyMMdd-HHmmss-ffff}{fileExtension}";
            File.WriteAllBytes(filePath, utfBytes);
            if (fileExtension == ".json" || fileExtension == ".xml" || fileExtension == ".html")
            {
                var filePathForAzurePreview = filePath + ".txt";
                if (File.Exists(filePathForAzurePreview))
                {
                    File.Delete(filePathForAzurePreview);
                }

                File.Move(filePath, filePathForAzurePreview);
                filePath = filePathForAzurePreview;
            }

            TestContext.AddTestAttachment(filePath);
        }

        public static void AddAttachmentAsJson(string name, object content, NullValueHandling nullValueHandling = NullValueHandling.Include)
        {
            var json = JsonConvert.SerializeObject(content, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = nullValueHandling });
            AddAttachment(name, JsonType, json, ".json");
        }
    }
}
