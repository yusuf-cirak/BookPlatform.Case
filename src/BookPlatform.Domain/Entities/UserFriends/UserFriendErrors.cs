using YC.Result;

namespace BookPlatform.Domain;

public static class UserFriendErrors
{
    public static readonly Error UserFriendAlreadyExists = Error.Create("User friend already exists");

    public static readonly Error UserFriendCannotBeAddedToItself =
        Error.Create("User friend cannot be added to itself");

    public static readonly Error CouldNotCreate = Error.Create("Could not create user friend");

    public static readonly Error CouldNotDelete = Error.Create("Could not delete user friend");
}