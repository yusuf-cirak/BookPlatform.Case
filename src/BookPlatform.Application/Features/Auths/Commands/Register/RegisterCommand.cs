using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Contracts.Auths;
using BookPlatform.Application.Features.Auths.Rules;
using BookPlatform.Application.Features.Auths.Services;
using BookPlatform.Infrastructure.Persistence.EntityFramework;
using MediatR;

namespace BookPlatform.Application.Features.Auths.Commands.Register;

public readonly record struct RegisterCommandRequest(string Username, string Password)
    : IRequest<Result<AuthResponseDto>>;

public sealed class
    RegisterUserCommandHandler : IRequestHandler<RegisterCommandRequest, Result<AuthResponseDto>>
{
    private readonly IEfRepository _efRepository;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IAuthService _authService;
    private readonly BaseService _baseService;

    public RegisterUserCommandHandler(AuthBusinessRules authBusinessRules, IEfRepository efRepository,
        IAuthService authService, BaseService baseService)
    {
        _authBusinessRules = authBusinessRules;
        _efRepository = efRepository;
        _authService = authService;
        _baseService = baseService;
    }

    public async Task<Result<AuthResponseDto>> Handle(RegisterCommandRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authBusinessRules.UserNameCannotBeDuplicatedBeforeRegistered(request.Username);

        if (result.IsFailure)
        {
            return result.Error;
        }

        _authService.RegisterUser(request.Username, request.Password, out var accessToken);

        await _baseService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var authResponseDto = new AuthResponseDto(accessToken.Token, accessToken.Expiration);

        return authResponseDto;
    }
}