using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GIBDemo.Core.Helpers;
using System.Collections.Generic;
using GIBDemo.Core.Models;

namespace GIBDemo.Triggers.Functions
{
    public static class ReadProductTrigger
    {
        [FunctionName(nameof(ReadProductTrigger))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ReadProduct/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName: Constants.COSMOS_DB_DATABASE_NAME,
                collectionName: Constants.COSMOS_DB_CONTAINER_NAME,
                ConnectionStringSetting = Constants.COSMOS_DB_CONNECTION_STRING,
                SqlQuery ="SELECT * FROM c WHERE c.id={id} ORDER BY c._ts DESC")] IEnumerable<Product> productItem,
            ILogger log,
            string id)
        {
            if (productItem == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(productItem);
        }
    }
}
