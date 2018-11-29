using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TailendersApi.Contracts;
using TailendersApi.WebApi.Extensions;
using TailendersApi.WebApi.Managers;

namespace TailendersApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : BaseController
    {
        private readonly IProfilesManager _profilesManager;

        public ProfilesController(IProfilesManager profilesManager)
        {
            _profilesManager = profilesManager;
        }

        // GET api/profiles/me
        [HttpGet("me")]
        public async Task<IActionResult> Get()
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
            var hasScope = User.HasRequiredScopes(WritePermission);
            if (!hasScope)
            {
                return Unauthorized();
            }

            var owner = User.CheckClaimMatch(ObjectIdElement);
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
                try
                {
                    savedImage = await _profilesManager.UploadProfileImage(id, file.FileName, ms.ToArray());
                }
                catch(Exception ex)
                {
                    throw; 
                }
            }
            return Ok(savedImage);
        }
    }
}
