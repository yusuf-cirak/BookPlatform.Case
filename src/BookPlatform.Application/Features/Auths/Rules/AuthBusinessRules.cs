using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Domain;
using BookPlatform.Infrastructure.Helpers.Hashing;
using BookPlatform.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Application.Features.Auths.Rules;

public sealed class AuthBusinessRules : BaseBusinessRules
{
    // FromServices attribute could be used instead of constructor injection
    private readonly IEfRepository _repository;

    public AuthBusinessRules(IEfRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> UserNameCannotBeDuplicatedBeforeRegistered(string username)
    {
        User? user = await _repository.Users.SingleOrDefaultAsync(user => user.Username == username);

        if (user is not null)
        {
            return Result.Failure(UserErrors.NameCannotBeDuplicated);
        }

        return Result.Success();
    }


    public Result UserCredentialsMustMatchBeforeLogin(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
        {
            return Result.Failure(UserErrors.WrongCredentials);
        }

        return Result.Success();
    }

    public async Task<Result<User>> UserNameShouldExistBeforeLoginAsync(string username,
        CancellationToken cancellationToken)
    {
        var user = await _repository
            .Users
            .SingleOrDefaultAsync(u => u.Username == username, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }

        return user;
    }
}