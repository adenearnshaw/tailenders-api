using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ProfilesController : BaseController
    {
        private readonly IProfilesManager _profilesManager;
        private readonly IMatchesManager _matchesManager;

        public ProfilesController(IProfilesManager profilesManager,
                                  IMatchesManager matchesManager)
        {
            _profilesManager = profilesManager;
            _matchesManager = matchesManager;
        }

        // GET api/profiles/d12cc0c1-c531-4e15-95a8-2093b951597d
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var hasScope = User.HasRequiredScopes(ReadPermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
            if (!owner.Equals(id))
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
            var hasScope = User.HasRequiredScopes(WritePermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
            if (!owner.Equals(model.Id))
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
            var hasScope = User.HasRequiredScopes(WritePermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
            if (!owner.Equals(model.Id))
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
            var hasScope = User.HasRequiredScopes(WritePermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
            if (!owner.Equals(id))
            {
                return Forbid();
            }

            await _profilesManager.DeleteProfile(id);

            return Ok();
        }

        // POST api/profiles/d12cc0c1-c531-4e15-95a8-2093b951597d/image
        [HttpPost("{id}/image")]
        public async Task<IActionResult> PostImage(string id, IFormFile file)
        {
            var hasScope = User.HasRequiredScopes(WritePermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
            if (owner != id)
            {
                return Forbid();
            }

            if (file == null || file.Length == 0)
            {
                return NoContent();
            }

            ProfileImage savedImage = null;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                savedImage = await _profilesManager.UploadProfileImage(id, file.FileName, ms.ToArray());
            }
            return Ok(savedImage);
        }

        // GET api/profiles/d12cc0c1-c531-4e15-95a8-2093b951597d/matches
        [HttpGet("{id}/matches")]
        public async Task<IActionResult> GetMatches(string id)
        {
            var hasScope = User.HasRequiredScopes(ReadPermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
            if (!owner.Equals(id))
            {
                return Forbid();
            }

            var matches = await _matchesManager.GetProfileMatches(id);

            return Ok(matches);
        }

        // POST api/profiles/report/d12cc0c1-c531-4e15-95a8-2093b951597d
        [HttpPost("report/{profileId}")]
        public async Task<IActionResult> ReportProfile(string profileId, [FromBody]ProfileReportInput input)
        {
            var hasScope = User.HasRequiredScopes(ReadPermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
            if (string.IsNullOrEmpty(owner))
            {
                return Forbid();
            }

            await _profilesManager.ReportProfile(profileId, input.ReportProfileReason);

            return Ok();
        }
    }
}
