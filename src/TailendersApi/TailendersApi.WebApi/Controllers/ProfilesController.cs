using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TailendersApi.Contracts;

namespace TailendersApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private static List<Profile> db = new List<Profile>();

        private const string ScopeElement = "http://schemas.microsoft.com/identity/claims/scope";
        private const string ObjectIdElement = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        private const string ReadPermission = "te.read.only";
        private const string WritePermission = "te.write.only";

        // GET api/profiles/me
        [HttpGet("me")]
        public ActionResult<Profile> Get()
        {
            //var hasScope = HasRequiredScopes(ReadPermission);
            //if (!hasScope)
            //{
            //    return Unauthorized();
            //}

            var owner = CheckClaimMatch(ObjectIdElement);
            if (string.IsNullOrEmpty(owner))
            {
                return BadRequest(
                    $"Unable to match claim '{ObjectIdElement}' against user claims; click the 'claims' tab to double-check.");
            }

            var profile = db.FirstOrDefault(p => p.Id == owner);
            return Ok(profile);
        }


        // POST api/profiles
        [HttpPost]
        public ActionResult<Profile> Post([FromBody] Profile model)
        {
            var owner = CheckClaimMatch(ObjectIdElement);
            if (owner != model.Id)
            {
                return Forbid();
            }

            db.Add(model);

            return Ok(model);
        }

        // PUT api/profiles
        [HttpPut]
        public ActionResult<Profile> Put([FromBody] Profile updatedModel)
        {
            var owner = CheckClaimMatch(ObjectIdElement);
            if (owner != updatedModel.Id)
            {
                return Forbid();
            }

            var oldModel = db.FirstOrDefault(p => string.Equals(p.Id, updatedModel.Id));
            if (oldModel == null)
            {
                return BadRequest("Cannot update. Profile with given ID does not exist");
            }

            db.Remove(oldModel);
            db.Add(updatedModel);

            return Ok(updatedModel);
        }

        // DELETE api/profiles/d12cc0c1-c531-4e15-95a8-2093b951597d
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var owner = CheckClaimMatch(ObjectIdElement);
            if (owner != id)
            {
                return Forbid();
            }

            var profileToDelete = db.FirstOrDefault(p => string.Equals(p.Id, id));
            if (profileToDelete == null)
            {
                return BadRequest("Cannot delete. Profile with given ID does not exist");
            }

            db.Remove(profileToDelete);
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
