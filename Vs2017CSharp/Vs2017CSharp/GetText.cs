using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Vs2017CSharp
{
    public static class GetText
    {
        [FunctionName("GetText")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "gettext/{id}")]
            HttpRequest req,
            [CosmosDB("testdb", "testcollection", Id = "{id}", ConnectionStringSetting = "CosmosDb")]
            dynamic document,
            string id,
            TraceWriter log)
        {
            return new OkObjectResult(document.text);
        }
    }
}