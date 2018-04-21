using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json.Linq;

namespace Vs2017CSharp
{
    public static class GetTranslation
    {
        [FunctionName("GetTranslation")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET")]
            HttpRequest req,
            TraceWriter log)
        {
            string text = req.Query["text"];

            using (var client = new DocumentClient(
                new Uri("https://localhost:8081"),
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="))
            {
                var collectionUri = UriFactory.CreateDocumentCollectionUri("testdb", "testcollection");
                var query = client.CreateDocumentQuery<JObject>(
                    collectionUri,
                    new SqlQuerySpec
                    {
                        QueryText = "SELECT c.translated FROM testcollection c WHERE (c.text = @text)",
                        Parameters = new SqlParameterCollection {new SqlParameter("@text", text)}
                    });

                return new OkObjectResult(query.ToList().First().Value<string>("translated"));
            }
        }
    }
}