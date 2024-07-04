using BookPlatform.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Application.Features.Users.Services;

public interface IUserService
{
    Task<int> UpdateUsernameAsync(string userId, string username, CancellationToken cancellationToken = default);
}

public sealed class UserService : IUserService, IScopedService
{
    private readonly IEfRepository _efRepository;

    public UserService(IEfRepository efRepository)
    {
        _efRepository = efRepository;
    }

    public Task<int> UpdateUsernameAsync(string userId, string username, CancellationToken cancellationToken = default)
    {
        return _efRepository
            .Users
            .Where(s => s.Id == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(u => u.Username, username), cancellationToken);
    }
}