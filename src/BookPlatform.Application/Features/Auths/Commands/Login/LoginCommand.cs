using BookPlatform.Application.Contracts.Auths;
using BookPlatform.Application.Contracts.JWT;
using BookPlatform.Application.Features.Auths.Rules;
using BookPlatform.Application.Features.Auths.Services;
using BookPlatform.Domain;
using BookPlatform.Infrastructure.Helpers.JWT;
using MediatR;

namespace BookPlatform.Application.Features.Auths.Commands.Login;

public readonly record struct LoginCommandRequest(string Username, string Password)
    : IRequest<Result<AuthResponseDto>>;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommandRequest, Result<AuthResponseDto>>
{
    private readonly IAuthService _authService;
    private readonly AuthBusinessRules _authBusinessRules;

    public LoginCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules)
    {
        _authService = authService;
        _authBusinessRules = authBusinessRules;
    }

    public async Task<Result<AuthResponseDto>> Handle(LoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authBusinessRules.UserNameShouldExistBeforeLoginAsync(request.Username, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error;
        }

        User user = result.Value;

        var verifyCredentialsResult =
            _authBusinessRules.UserCredentialsMustMatchBeforeLogin(request.Password, user.PasswordHash,
                user.PasswordSalt);

        if (verifyCredentialsResult.IsFailure)
        {
            return verifyCredentialsResult.Error;
        }


        var userClaims = await _authService.GetUserClaimsAsync(user.Id, cancellationToken);


        AccessToken accessToken = JwtHelper.CreateAccessToken(user, userClaims);

        var authResponseDto = new AuthResponseDto(accessToken.Token, accessToken.Expiration);

        return authResponseDto;
    }
}