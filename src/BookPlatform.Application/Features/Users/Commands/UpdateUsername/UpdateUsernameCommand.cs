using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.Auths.Rules;
using BookPlatform.Application.Features.Users.Services;
using BookPlatform.Domain;
using MediatR;

namespace BookPlatform.Application.Features.Users.Commands.UpdateUsername;

public sealed record UpdateUsernameCommandRequest(string Username) : IRequest<Result>, ISecuredRequest;

public sealed class UpdateUsernameCommandHandler : IRequestHandler<UpdateUsernameCommandRequest, Result>
{
    private readonly BaseService _baseService;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IUserService _userService;

    public UpdateUsernameCommandHandler(BaseService baseService, AuthBusinessRules authBusinessRules,
        IUserService userService)
    {
        _baseService = baseService;
        _authBusinessRules = authBusinessRules;
        _userService = userService;
    }

    public async Task<Result> Handle(UpdateUsernameCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = _baseService.CurrentUser.GetUserId();

        var canUpdate = await _authBusinessRules.UserNameCannotBeDuplicatedBeforeRegistered(request.Username);

        if (canUpdate.IsFailure)
        {
            return canUpdate.Error;
        }

        var res = await _userService.UpdateUsernameAsync(currentUserId, request.Username, cancellationToken);

        return res > 0 ? Result.Success() : UserErrors.CantUpdateUsername;
    }
}