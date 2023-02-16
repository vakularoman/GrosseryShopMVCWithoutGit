namespace AquaPlayground
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtension
    {
        public static long GetUserId(this ClaimsPrincipal claim)
        {
            return long.Parse(claim.Claims.First(x => x.Type == ClaimTypes.NameIdentifier)?.Value!);
        }

        public static bool TryGetUserId(this ClaimsPrincipal claim, out long userId)
        {
            return long.TryParse(claim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!, out userId);
        }
    }
}