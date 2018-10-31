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
    public static class ProfilesEndpoint
    {
        [FunctionName("GetProfile")]
        public static async Task<IActionResult> GetProfile(
            [HttpTrigger(AuthorizationLevel.User, "get", Route = "profiles/me")] HttpRequest req,
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

        [FunctionName("UpdateProfile")]
        public static async Task<IActionResult> UpdateProfile(
            [HttpTrigger(AuthorizationLevel.User, "put", Route = "profiles/me")] HttpRequest req,
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

        [FunctionName("GetProfiles")]
        public static async Task<IActionResult> GetProfiles(
            [HttpTrigger(AuthorizationLevel.User, "get", Route = "profiles")] HttpRequest req,
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

        [FunctionName("LikeProfile")]
        public static async Task<IActionResult> LikeProfile(
            [HttpTrigger(AuthorizationLevel.User, "post", Route = "profiles/{id}/like")] HttpRequest req,
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

        [FunctionName("DislikeProfile")]
        public static async Task<IActionResult> DislikeProfile(
            [HttpTrigger(AuthorizationLevel.User, "post", Route = "profiles/{id}/dislike")] HttpRequest req,
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
