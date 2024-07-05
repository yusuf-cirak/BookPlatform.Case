namespace BookPlatform.Domain.UnitTests.UserFriends;

public class UserFriendTests
{
    [Fact]
    public void Create_ShouldReturnUserFriendWithCorrectProperties()
    {
        // Arrange
        var userId = "user123";
        var friendUserId = "friend123";

        // Act
        var userFriend = UserFriend.Create(userId, friendUserId);

        // Assert
        Assert.NotNull(userFriend);
        Assert.Equal(userId, userFriend.UserId);
        Assert.Equal(friendUserId, userFriend.FriendUserId);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectString()
    {
        // Arrange
        var userFriend = new UserFriend("user123", "friend123");

        // Act
        var result = userFriend.ToString();

        // Assert
        Assert.Equal("user123 - friend123", result);
    }

    [Fact]
    public void Equals_ShouldReturnTrueForSameUserFriend()
    {
        // Arrange
        var userFriend1 = new UserFriend("user123", "friend123");
        var userFriend2 = new UserFriend("user123", "friend123");

        // Act
        var result = userFriend1.Equals(userFriend2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ShouldReturnFalseForDifferentUserFriend()
    {
        // Arrange
        var userFriend1 = new UserFriend("user123", "friend123");
        var userFriend2 = new UserFriend("user123", "friend456");

        // Act
        var result = userFriend1.Equals(userFriend2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_ShouldReturnFalseForNull()
    {
        // Arrange
        var userFriend = new UserFriend("user123", "friend123");

        // Act
        var result = userFriend.Equals(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameHashCodeForSameUserFriend()
    {
        // Arrange
        var userFriend1 = new UserFriend("user123", "friend123");
        var userFriend2 = new UserFriend("user123", "friend123");

        // Act
        var hash1 = userFriend1.GetHashCode();
        var hash2 = userFriend2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnDifferentHashCodeForDifferentUserFriend()
    {
        // Arrange
        var userFriend1 = new UserFriend("user123", "friend123");
        var userFriend2 = new UserFriend("user123", "friend456");

        // Act
        var hash1 = userFriend1.GetHashCode();
        var hash2 = userFriend2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }
}