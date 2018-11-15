using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TailendersApi.Contracts;
using TailendersApi.WebApi.Managers;

namespace TailendersApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {

        private const string ScopeElement = "http://schemas.microsoft.com/identity/claims/scope";
        private const string ObjectIdElement = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        private const string ReadPermission = "te.read";
        private const string WritePermission = "te.write";

        private readonly IProfilesManager _profilesManager;

        public ProfilesController(IProfilesManager profilesManager)
        {
            _profilesManager = profilesManager;
        }

        // GET api/profiles/me
        [HttpGet("me")]
        public async Task<IActionResult> Get()
        {
            var hasScope = HasRequiredScopes(ReadPermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = CheckClaimMatch(ObjectIdElement);
            if (string.IsNullOrEmpty(owner))
            {
                return BadRequest(
                    $"Unable to match claim '{ObjectIdElement}' against user claims; click the 'claims' tab to double-check.");
            }

            var profile = await _profilesManager.GetProfile(owner);
            return Ok(profile);
        }


        // POST api/profiles
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Profile model)
        {
            var hasScope = HasRequiredScopes(WritePermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = CheckClaimMatch(ObjectIdElement);
            if (owner != model.Id)
            {
                return Forbid();
            }

            var savedProfile = await _profilesManager.AddProfile(model);

            return Ok(savedProfile);
        }

        // PUT api/profiles
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Profile model)
        {
            var hasScope = HasRequiredScopes(WritePermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = CheckClaimMatch(ObjectIdElement);
            if (owner != model.Id)
            {
                return Forbid();
            }

            var savedProfile = await _profilesManager.UpdateProfile(model);

            return Ok(savedProfile);
        }

        // DELETE api/profiles/d12cc0c1-c531-4e15-95a8-2093b951597d
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var hasScope = HasRequiredScopes(WritePermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = CheckClaimMatch(ObjectIdElement);
            if (owner != id)
            {
                return Forbid();
            }

            await _profilesManager.DeleteProfile(id);

            return Ok();
        }

        
        // Check user claims match task details
        private string CheckClaimMatch(string claim)
        {
            try
            {
                return User.FindFirst(claim).Value;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // Validate to ensure the necessary scopes are present.
        private bool HasRequiredScopes(String permission)
        {
            return User.FindFirst(ScopeElement).Value.Contains(permission);
        }
    }
}
