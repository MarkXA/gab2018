using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Vs2017CSharp
{
    public static class AddText
    {
        [FunctionName("AddText")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET")]
            HttpRequest req,
            [CosmosDB("testdb", "testcollection", Id = "id", ConnectionStringSetting = "CosmosDb")]
            ICollector<dynamic> newDocs,
            TraceWriter log)
        {
            string text = req.Query["text"];

            newDocs.Add(new {id = Guid.NewGuid().ToString(), text});

            return new OkObjectResult($"Added '{text}'");
        }
    }
}