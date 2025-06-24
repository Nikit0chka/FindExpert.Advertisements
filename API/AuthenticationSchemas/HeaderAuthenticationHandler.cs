using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace API.AuthenticationSchemas;

public sealed class HeaderAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    :AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("X-User-Id", out var userId) ||
            !Request.Headers.TryGetValue("X-User-Role", out var userRole))
            return Task.FromResult(AuthenticateResult.Fail("Required headers (userId, userRole) are missing"));

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userRole))
            return Task.FromResult(AuthenticateResult.Fail("Invalid user ID or role"));

        var claims = new[]
                     {
                         new Claim(ClaimTypes.NameIdentifier, userId!),
                         new Claim(ClaimTypes.Role, userRole!)
                     };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}