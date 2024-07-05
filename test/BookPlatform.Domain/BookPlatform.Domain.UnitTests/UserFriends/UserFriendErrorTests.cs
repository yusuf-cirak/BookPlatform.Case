namespace BookPlatform.Domain.UnitTests.UserFriends;

public class UserFriendErrorsTests
{
    [Fact]
    public void UserFriendAlreadyExists_ShouldHaveCorrectMessage()
    {
        // Arrange & Act
        var error = UserFriendErrors.UserFriendAlreadyExists;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("User friend already exists", error.Detail);
    }

    [Fact]
    public void UserFriendCannotBeAddedToItself_ShouldHaveCorrectMessage()
    {
        // Arrange & Act
        var error = UserFriendErrors.UserFriendCannotBeAddedToItself;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("User friend cannot be added to itself", error.Detail);
    }

    [Fact]
    public void CouldNotCreate_ShouldHaveCorrectMessage()
    {
        // Arrange & Act
        var error = UserFriendErrors.CouldNotCreate;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("Could not create user friend", error.Detail);
    }

    [Fact]
    public void CouldNotDelete_ShouldHaveCorrectMessage()
    {
        // Arrange & Act
        var error = UserFriendErrors.CouldNotDelete;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("Could not delete user friend", error.Detail);
    }
}