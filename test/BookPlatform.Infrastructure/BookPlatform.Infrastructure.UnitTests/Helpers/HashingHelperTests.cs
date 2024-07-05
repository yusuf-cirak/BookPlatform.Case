using BookPlatform.Infrastructure.Helpers.Hashing;

namespace BookPlatform.Infrastructure.UnitTests.Helpers;

public class HashingHelperTests
{
    [Fact]
    public void CreatePasswordHash_ShouldGeneratePasswordHashAndSalt()
    {
        // Arrange
        var password = "testpassword";

        // Act
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        // Assert
        Assert.NotNull(passwordHash);
        Assert.NotEmpty(passwordHash);
        Assert.NotNull(passwordSalt);
        Assert.NotEmpty(passwordSalt);
    }

    [Fact]
    public void VerifyPasswordHash_ShouldReturnTrueForCorrectPassword()
    {
        // Arrange
        var password = "testpassword";
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        // Act
        var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPasswordHash_ShouldReturnFalseForIncorrectPassword()
    {
        // Arrange
        var password = "testpassword";
        var incorrectPassword = "wrongpassword";
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        // Act
        var result = HashingHelper.VerifyPasswordHash(incorrectPassword, passwordHash, passwordSalt);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void VerifyPasswordHash_ShouldReturnFalseForDifferentSalt()
    {
        // Arrange
        var password = "testpassword";
        var anotherPassword = "anotherpassword";
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        HashingHelper.CreatePasswordHash(anotherPassword, out _, out byte[] anotherPasswordSalt);

        // Act
        var result = HashingHelper.VerifyPasswordHash(password, passwordHash, anotherPasswordSalt);

        // Assert
        Assert.False(result);
    }
}