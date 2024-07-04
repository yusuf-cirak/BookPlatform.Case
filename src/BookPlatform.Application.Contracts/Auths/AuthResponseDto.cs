namespace BookPlatform.Application.Contracts.Auths;

public sealed record AuthResponseDto(string Token, DateTime Expiration);