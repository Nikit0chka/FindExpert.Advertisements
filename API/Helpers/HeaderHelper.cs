using Ardalis.Result;

namespace API.Helpers;

internal static class HeaderHelper
{
    public static Result<int> GetUserIdFromHeader(HttpContext httpContext)
    {
        var userIdHeader = httpContext.Request.Headers["X-User-Id"].FirstOrDefault();

        if (string.IsNullOrEmpty(userIdHeader))
            return Result.Error("User ID header is missing or empty.");

        if (!int.TryParse(userIdHeader, out var userId))
            return Result.Error("Invalid User ID format.");

        return userId;
    }
}