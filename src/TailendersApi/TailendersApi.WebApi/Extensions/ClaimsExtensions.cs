using System;
using System.Security.Claims;

namespace TailendersApi.WebApi.Extensions
{
    public static class ClaimsExtensions
    {
        private const string ScopeElement = "http://schemas.microsoft.com/identity/claims/scope";

        // Check user claims match task details
        public static string CheckClaimMatch(this ClaimsPrincipal user, string claim)
        {
            try
            {
                return user.FindFirst(claim).Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Validate to ensure the necessary scopes are present.
        public static bool HasRequiredScopes(this ClaimsPrincipal user, string permission)
        {
            return user.FindFirst(ScopeElement).Value.Contains(permission);
        }
    }
}
