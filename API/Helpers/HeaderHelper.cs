using Domain.AggregateModels.ValueObjects;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Helpers;

internal static class HeaderHelper
{
    public static int? GetUserIdFromHeader(HttpContext httpContext)
    {
        var userIdHeader = httpContext.Request.Headers["X-User-Id"].FirstOrDefault();

        if (userIdHeader is null)
            return null;

        return int.Parse(userIdHeader);
    }

    public static ICollection<Role> GetUserRoleFromHeader(HttpContext httpContext)
    {
        var userIdHeader = httpContext.Request.Headers["X-User-Role"].FirstOrDefault();

        var roles = JsonSerializer.Deserialize<ICollection<Role>>(userIdHeader!);

        return roles!;
    }
}