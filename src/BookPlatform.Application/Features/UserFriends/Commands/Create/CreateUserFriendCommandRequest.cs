using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.UserFriends.Services;
using BookPlatform.Domain;
using MediatR;

namespace BookPlatform.Application.Features.UserFriends.Commands.Create;

public sealed record CreateUserFriendCommandRequest(string UserId) : IRequest<Result>, ISecuredRequest;

public sealed class CreateUserFriendCommandHandler : IRequestHandler<CreateUserFriendCommandRequest, Result>
{
    private readonly BaseService _baseService;
    private readonly IUserFriendService _userFriendService;

    public CreateUserFriendCommandHandler(BaseService baseService, IUserFriendService userFriendService)
    {
        _baseService = baseService;
        _userFriendService = userFriendService;
    }

    public async Task<Result> Handle(CreateUserFriendCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = _baseService.CurrentUser.GetUserId();

        var userFriendRes = _userFriendService.CreateUserFriend(currentUserId, request.UserId);

        if (userFriendRes.IsFailure)
        {
            return userFriendRes.Error;
        }

        var res = await _baseService.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;


        return res switch
        {
            true => Result.Success(),
            _ => UserFriendErrors.CouldNotCreate
        };
    }
}