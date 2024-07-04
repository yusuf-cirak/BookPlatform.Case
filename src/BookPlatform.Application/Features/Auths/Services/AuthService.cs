using System.Security.Claims;
using BookPlatform.Application.Contracts.JWT;
using BookPlatform.Domain;
using BookPlatform.Infrastructure.Helpers.Hashing;
using BookPlatform.Infrastructure.Helpers.JWT;
using BookPlatform.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Application.Features.Auths.Services;

public sealed class AuthService : IAuthService, IScopedService
{
    private readonly IEfRepository _efRepository;

    public AuthService(IEfRepository efRepository)
    {
        _efRepository = efRepository;
    }

    public User RegisterUser(string username, string password, out AccessToken accessToken)
    {
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        User user = User.Create(username, passwordHash, passwordSalt);


        accessToken = JwtHelper.CreateAccessToken(user, []);

        _efRepository.Users.Add(user);

        return user;
    }

    public Task<List<Claim>> GetUserClaimsAsync(string userId, CancellationToken cancellationToken)
    {
        return this
            .GetUserFriendsQueryable(userId)
            .Select(uf => new Claim("Friends", uf.FriendUserId != userId ? uf.FriendUserId : uf.UserId))
            .ToListAsync(cancellationToken);
    }


    public IQueryable<UserFriend> GetUserFriendsQueryable(string userId,
        CancellationToken cancellationToken = default)
    {
        return _efRepository
            .UserFriends
            .Where(uf => uf.UserId == userId || uf.FriendUserId == userId);
    }
}