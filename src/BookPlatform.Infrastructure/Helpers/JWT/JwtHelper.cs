using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookPlatform.Application.Contracts.JWT;
using BookPlatform.Domain;
using BookPlatform.Infrastructure.Helpers.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookPlatform.Infrastructure.Helpers.JWT;

public static class JwtHelper
{
    internal static IConfigurationRoot Configuration { get; set; }

    internal static TokenOptions TokenOptions { get; set; }

    internal static DateTime _accessTokenExpiration;

    static JwtHelper()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true, reloadOnChange: true)
            .Build();

        TokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>()!;
    }


    public static AccessToken CreateAccessToken(User user, IEnumerable<Claim> claims)
    {
        _accessTokenExpiration = DateTime.UtcNow.AddMinutes(TokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(TokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(user, signingCredentials, claims);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string token = jwtSecurityTokenHandler.WriteToken(jwt);
        return new AccessToken(token, _accessTokenExpiration);
    }

    internal static JwtSecurityToken CreateJwtSecurityToken(User user,
        SigningCredentials signingCredentials, IEnumerable<Claim> additionalClaims)
    {
        JwtSecurityToken jwt = new(
            TokenOptions.Issuer,
            TokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: SetClaims(user, additionalClaims)
        );
        return jwt;
    }

    internal static IEnumerable<Claim> SetClaims(User user, IEnumerable<Claim> additionalClaims)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        ];

        claims.AddRange(additionalClaims);
        return claims;
    }
}