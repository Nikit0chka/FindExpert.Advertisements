using System.Security.Claims;

namespace API.Middlewares;

internal sealed class AuthorizationMiddleware(RequestDelegate next)
{
    public Task InvokeAsync(HttpContext context)
    {
        var userId = context.Request.Headers["X-User-Id"].FirstOrDefault();
        var userRole = context.Request.Headers["X-User-Role"].FirstOrDefault();

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userRole))
            return next(context);

        var claims = new List<Claim>
                     {
                         new(ClaimTypes.NameIdentifier, userId),
                         new(ClaimTypes.Role, userRole)
                     };

        var identity = new ClaimsIdentity(claims, "CustomHeaderAuth");
        context.User = new(identity);

        return next(context);
    }
}