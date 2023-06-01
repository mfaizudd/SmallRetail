using System.Security.Claims;

namespace SmallRetail.WebApi.Helpers
{
    public static class ClaimUtils
    {
        public static string? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue("uid");
        }
    }
}
