using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TailendersApi.Contracts;
using TailendersApi.WebApi.Extensions;
using TailendersApi.WebApi.Managers;

namespace TailendersApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : BaseController
    {
        private readonly IMatchesManager _matchesManager;

        public MatchesController(IMatchesManager matchesManager)
        {
            _matchesManager = matchesManager;
        }

        //PUT api/matches
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MatchDetail model)
        {
            var hasScope = User.HasRequiredScopes(WritePermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
            if (!owner.Equals(model.ProfileId))
            {
                return Forbid();
            }

            var savedMatch = await _matchesManager.UpdateMatch(model);

            return Ok(savedMatch);
        }

        // DELETE api/matches/38f269ad-75fd-4695-b63a-04eb2d61b21e/unmatch/
        [HttpDelete("{matchId}/unmatch")]
        public async Task<IActionResult> Unmatch(string matchId)
        {
            var hasScope = User.HasRequiredScopes(WritePermission);
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

            await _matchesManager.UnmatchProfile(owner, matchId);

            return Ok();
        }
    }
}
