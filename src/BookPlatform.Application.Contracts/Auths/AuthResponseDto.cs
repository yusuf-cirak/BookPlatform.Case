namespace BookPlatform.Application.Contracts.Auths;

public sealed class AuthResponseDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    
    public AuthResponseDto(string token, DateTime expiration)
    {
        Token = token;
        Expiration = expiration;
    }
}