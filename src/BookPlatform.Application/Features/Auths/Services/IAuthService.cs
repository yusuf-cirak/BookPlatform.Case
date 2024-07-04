using System.Security.Claims;
using BookPlatform.Application.Contracts.JWT;
using BookPlatform.Domain;

namespace BookPlatform.Application.Features.Auths.Services;

public interface IAuthService
{
    User RegisterUser(string username, string password, out AccessToken accessToken);

    Task<List<Claim>> GetUserClaimsAsync(string userId,
        CancellationToken cancellationToken);


    IQueryable<UserFriend> GetUserFriendsQueryable(string userId,
        CancellationToken cancellationToken);
}