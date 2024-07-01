using WebApplication1.Helpers;
using WebApplication1.Models.User;

namespace WebApplication1.utils;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Security.Claims;

public class CustomAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    JwtHelper jwtHelper
)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token) || !jwtHelper.ValidateToken(token) )
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Token"));
        }


        var customUser = new UserModel
        {
            FirstName = "Users first name",
            LastName = "Users last name"
        };

        // Set the user identity if the token is valid


        var identity = new CustomClaimsIdentity(new List<Claim>(), customUser);
        var principal = new CustomClaimsPrincipal(identity, customUser);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }


}