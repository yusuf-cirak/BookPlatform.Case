namespace BookPlatform.Domain.UnitTests.Users;

public class UserTests
{
    [Fact]
    public void Create_ShouldReturnUserWithCorrectProperties()
    {
        // Arrange
        var username = "testuser";
        var passwordHash = new byte[] { 1, 2, 3, 4, 5 };
        var passwordSalt = new byte[] { 6, 7, 8, 9, 10 };

        // Act
        var user = User.Create(username, passwordHash, passwordSalt);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(username, user.Username);
        Assert.Equal(passwordHash, user.PasswordHash);
        Assert.Equal(passwordSalt, user.PasswordSalt);
    }

    [Fact]
    public void Username_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var user = new User { Username = "testuser" };

        // Act
        user.Username = "newusername";

        // Assert
        Assert.Equal("newusername", user.Username);
    }

    [Fact]
    public void PasswordHash_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var passwordHash = new byte[] { 1, 2, 3, 4, 5 };
        var user = new User { PasswordHash = passwordHash };

        // Act
        var newPasswordHash = new byte[] { 5, 4, 3, 2, 1 };
        user.PasswordHash = newPasswordHash;

        // Assert
        Assert.Equal(newPasswordHash, user.PasswordHash);
    }

    [Fact]
    public void PasswordSalt_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var passwordSalt = new byte[] { 1, 2, 3, 4, 5 };
        var user = new User { PasswordSalt = passwordSalt };

        // Act
        var newPasswordSalt = new byte[] { 5, 4, 3, 2, 1 };
        user.PasswordSalt = newPasswordSalt;

        // Assert
        Assert.Equal(newPasswordSalt, user.PasswordSalt);
    }

    [Fact]
    public void UserFriends_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var userFriends = new List<UserFriend> { new UserFriend("user123", "friend123") };
        var user = new User { UserFriends = userFriends };

        // Act
        var newUserFriends = new List<UserFriend> { new UserFriend("user456", "friend456") };
        user.UserFriends = newUserFriends;

        // Assert
        Assert.Equal(newUserFriends, user.UserFriends);
    }
}