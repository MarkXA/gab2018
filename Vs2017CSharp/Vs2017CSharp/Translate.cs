using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Vs2017CSharp
{
    public static class Translate
    {
        [FunctionName("Translate")]
        public static void Run(
            [CosmosDBTrigger("testdb", "testcollection", ConnectionStringSetting = "CosmosDB", CreateLeaseCollectionIfNotExists = true)]
            IReadOnlyList<Document> documents,
            [CosmosDB("testdb", "testcollection", Id = "id", ConnectionStringSetting = "CosmosDb")]
            ICollector<dynamic> translatedDocs,
            TraceWriter log)
        {
            foreach (var doc in documents)
                if (doc.GetPropertyValue<object>("translated") == null)
                {
                    doc.SetPropertyValue(
                        "translated",
                        new string(doc.GetPropertyValue<string>("text").Reverse().ToArray()));
                    translatedDocs.Add(doc);
                }
        }
    }
}