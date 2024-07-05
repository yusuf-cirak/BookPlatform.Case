using YC.Result;

namespace BookPlatform.Domain;

public readonly record struct UserErrors
{
    public static readonly Error NameCannotBeDuplicated = Error.Create("User.NameCannotBeDuplicated",
        "User name cannot be duplicated");

    public static readonly Error NotFound = Error.Create("User.NotFound", "User not found");

    public static readonly Error WrongCredentials =
        Error.Create("User.WrongCredentials", "User credentials does not match");

    public static readonly Error CantUpdateUsername =
        Error.Create("User.CantUpdateUsername", "Can't update username");
}