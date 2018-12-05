using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TailendersApi.Contracts;
using TailendersApi.WebApi.Extensions;
using TailendersApi.WebApi.Managers;
using TailendersApi.WebApi.Model;

namespace TailendersApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PairingsController : BaseController
    {
        private readonly IProfilesManager _profilesManager;
        private readonly IPairingsManager _pairingsManager;

        public PairingsController(IProfilesManager profilesManager,
                                  IPairingsManager pairingsManager)
        {
            _profilesManager = profilesManager;
            _pairingsManager = pairingsManager;
        }

        // GET
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, 
                                             [FromQuery]int category,
                                             [FromQuery]int minAge,
                                             [FromQuery]int maxAge,
                                             [FromQuery]double lat,
                                             [FromQuery]double lon,
                                             [FromQuery]int take)
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
            var results = await _profilesManager.SearchForProfiles(id);
            return new OkObjectResult(results);
        }

        // POST
        [HttpPost("{id}/decision")]
        public async Task<IActionResult> PostDecision(string id, 
                                                      [FromBody] PairingDecisionInput input)
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

            if (string.IsNullOrEmpty(input.PairProfileId))
            {
                return BadRequest($"Parameter: {nameof(input.PairProfileId)} is null");
            }
            if (default(PairingDecision).Equals(input.Decision))
            {
                return BadRequest($"Parameter: {nameof(input.Decision)} is null");
            }

            var result = await _pairingsManager.SetPairingDescision(id, input.PairProfileId, input.Decision);
            return new OkObjectResult(result);
        }
    }
}
