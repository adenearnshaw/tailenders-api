using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TailendersApi.WebApi.Extensions;
using TailendersApi.WebApi.Managers;

namespace TailendersApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PairingsController : BaseController
    {
        private readonly IProfilesManager _profilesManager;

        public PairingsController(IProfilesManager profilesManager)
        {
            _profilesManager = profilesManager;
        }

        // GET
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var hasScope = User.HasRequiredScopes(ReadPermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
            if (string.IsNullOrEmpty(owner))
            {
                return BadRequest(
                    $"Unable to match claim '{ObjectIdElement}' against user claims; click the 'claims' tab to double-check.");
            }

            //TODO: Search for profiles not matched
        }
    }
}
