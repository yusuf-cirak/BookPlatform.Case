using System.Security.Claims;

namespace BookPlatform.Application.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static IEnumerable<string> GetFriends(this ClaimsPrincipal user)
    {
        return user.FindAll("Friends").Select(f => f.Value);
    }

    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    }
}