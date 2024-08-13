using System.Security.Claims;

namespace BankingControlPanel.Api.Helpers;

public static class UserExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        // Check if the user has the NameIdentifier claim
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value??"0";
    }
}