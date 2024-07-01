namespace WebApplication1.Helpers;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtHelper(IConfiguration configuration)
{
    public string GenerateToken(string userId, string email)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var secretKey = jwtSettings["Key"] ?? string.Empty;
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "30");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var secretKey = jwtSettings["Key"] ?? string.Empty;
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateLifetime = true, // Validate the token's expiration
            ClockSkew = TimeSpan.Zero // Remove delay of token when expire
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Ensure the token is a valid JWT token
            if (validatedToken is JwtSecurityToken jwtToken)
            {
                return jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}