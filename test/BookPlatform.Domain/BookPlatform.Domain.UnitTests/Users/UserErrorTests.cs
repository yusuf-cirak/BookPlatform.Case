namespace BookPlatform.Domain.UnitTests.Users;

public class UserErrorsTests
{
    [Fact]
    public void NameCannotBeDuplicated_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var error = UserErrors.NameCannotBeDuplicated;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("User.NameCannotBeDuplicated", error.Title);
        Assert.Equal("User name cannot be duplicated", error.Detail);
    }

    [Fact]
    public void NotFound_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var error = UserErrors.NotFound;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("User.NotFound", error.Title);
        Assert.Equal("User not found", error.Detail);
    }

    [Fact]
    public void WrongCredentials_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var error = UserErrors.WrongCredentials;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("User.WrongCredentials", error.Title);
        Assert.Equal("User credentials does not match", error.Detail);
    }

    [Fact]
    public void CantUpdateUsername_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var error = UserErrors.CantUpdateUsername;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("User.CantUpdateUsername", error.Title);
        Assert.Equal("Can't update username", error.Detail);
    }
}