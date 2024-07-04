namespace BookPlatform.Application.Contracts.JWT;

public sealed record AccessToken(string Token,DateTime Expiration);