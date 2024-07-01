using WebApplication1.Models.User;

namespace WebApplication1.utils;

using System.Security.Claims;

public class CustomClaimsPrincipal(ClaimsIdentity identity, UserModel user) : ClaimsPrincipal(identity)
{
    public UserModel CustomUser { get; } = user;
}

public class CustomClaimsIdentity(IEnumerable<Claim> claims, UserModel user) : ClaimsIdentity(claims, "Custom")
{
    public UserModel UserModel { get; } = user;
}