using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace TailendersApi.Functions.Endpoints
{
    public static class MatchesEndpoint
    {
        [FunctionName("GetMatches")]
        public static async Task<IActionResult> GetMatches(
            [HttpTrigger(AuthorizationLevel.User, "get", Route = "matches")] HttpRequest req,
            [Token(Identity = TokenIdentityMode.ClientCredentials, Resource = "")] string token,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        [FunctionName("UnMatch")]
        public static async Task<IActionResult> UnMatch(
            [HttpTrigger(AuthorizationLevel.User, "post", Route = "matches/{id}/unmatch")] HttpRequest req,
            [Token(Identity = TokenIdentityMode.ClientCredentials, Resource = "")] string token,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        [FunctionName("SendMessage")]
        public static async Task<IActionResult> SendMessage(
            [HttpTrigger(AuthorizationLevel.User, "post", Route = "matches/{id}/message")] HttpRequest req,
            [Token(Identity = TokenIdentityMode.ClientCredentials, Resource = "")] string token,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
