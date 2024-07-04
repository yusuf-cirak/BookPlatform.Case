using BookPlatform.Application.Common.Services;
using BookPlatform.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Application.Features.UserFriends.Services;

public interface IUserFriendService
{
    Result<UserFriend> CreateUserFriend(string userId, string friendUserId);

    Task<UserFriend?> GetUserFriendAsync(string userId);

    Task<List<string>> GetUserFriendsAsync(string userId);

    Task<int> DeleteUserFriendAsync(string userId, string friendUserId,
        CancellationToken cancellationToken = default);
}

public sealed class UserFriendService : BaseService, IUserFriendService
{
    public Result<UserFriend> CreateUserFriend(string userId, string friendUserId)
    {
        if (userId == friendUserId)
        {
            return UserFriendErrors.UserFriendCannotBeAddedToItself;
        }

        var userFriend = UserFriend.Create(userId, friendUserId);

        EfRepository.UserFriends.Add(userFriend);

        return userFriend;
    }

    public Task<UserFriend?> GetUserFriendAsync(string userId)
    {
        return EfRepository.UserFriends.SingleOrDefaultAsync(x => x.UserId == userId);
    }

    public Task<List<string>> GetUserFriendsAsync(string userId)
    {
        return EfRepository
            .UserFriends
            .Where(x => x.UserId == userId || x.FriendUserId == userId)
            .Select(x => x.UserId == userId ? x.FriendUserId : x.UserId)
            .ToListAsync();
    }

    public Task<int> DeleteUserFriendAsync(string userId, string friendUserId,
        CancellationToken cancellationToken = default)
    {
        return EfRepository
            .UserFriends
            .Where(s => (s.UserId == userId && s.FriendUserId == friendUserId) || (s.UserId == userId &&
                s.FriendUserId == userId))
            .ExecuteDeleteAsync(cancellationToken);
    }
}