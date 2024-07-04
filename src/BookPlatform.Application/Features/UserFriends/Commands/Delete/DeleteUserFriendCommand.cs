using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.UserFriends.Services;
using BookPlatform.Domain;
using MediatR;

namespace BookPlatform.Application.Features.UserFriends.Commands.Delete;

public sealed record DeleteUserFriendCommandRequest(string Id) : IRequest<Result>, ISecuredRequest;

public sealed class DeleteUserFriendCommandHandler : IRequestHandler<DeleteUserFriendCommandRequest, Result>
{
    private readonly BaseService _baseService;
    private readonly IUserFriendService _userFriendService;

    public DeleteUserFriendCommandHandler(BaseService baseService, IUserFriendService userFriendService)
    {
        _baseService = baseService;
        _userFriendService = userFriendService;
    }

    public async Task<Result> Handle(DeleteUserFriendCommandRequest request, CancellationToken cancellationToken)
    {
        var userId = _baseService.CurrentUser.GetUserId();

        var deleted = await _userFriendService.DeleteUserFriendAsync(userId, request.Id, cancellationToken) > 0;

        return deleted switch
        {
            true => Result.Success(),
            _ => UserFriendErrors.CouldNotDelete
        };
    }
}