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
    private static IConfigurationRoot Configuration { get; }

    private static TokenOptions TokenOptions { get; }
    
    private static DateTime _accessTokenExpiration;

    static JwtHelper()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true)
            .Build();
        
        TokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>()!;
    }



    public static AccessToken CreateAccessToken(User user, IEnumerable<Claim> claims)
    {
        _accessTokenExpiration = DateTime.UtcNow.AddMinutes(TokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(TokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(TokenOptions, user, signingCredentials, claims);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string token = jwtSecurityTokenHandler.WriteToken(jwt);
        return new AccessToken(token, _accessTokenExpiration);
    }

    private static JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
        SigningCredentials signingCredentials, IEnumerable<Claim> additionalClaims)
    {
        JwtSecurityToken jwt = new(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: SetClaims(user, additionalClaims)
        );
        return jwt;
    }

    private static IEnumerable<Claim> SetClaims(User user, IEnumerable<Claim> additionalClaims)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        ];

        claims.AddRange(additionalClaims);
        return claims;
    }
}