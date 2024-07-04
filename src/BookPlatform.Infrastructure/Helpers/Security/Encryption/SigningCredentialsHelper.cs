using Microsoft.IdentityModel.Tokens;

namespace BookPlatform.Infrastructure.Helpers.Security.Encryption;

public static class SigningCredentialsHelper
{
    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
    {
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
    }
}