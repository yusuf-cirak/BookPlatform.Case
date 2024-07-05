namespace BookPlatform.Domain.UnitTests.Books;

public class BookErrorsTests
{
    [Fact]
    public void FailedToCreate_ShouldHaveCorrectMessage()
    {
        // Arrange & Act
        var error = BookErrors.FailedToCreate;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("Failed to create book", error.Detail);
    }

    [Fact]
    public void FailedToUpdate_ShouldHaveCorrectMessage()
    {
        // Arrange & Act
        var error = BookErrors.FailedToUpdate;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("Failed to update book", error.Detail);
    }

    [Fact]
    public void FailedToDelete_ShouldHaveCorrectMessage()
    {
        // Arrange & Act
        var error = BookErrors.FailedToDelete;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("Failed to delete book", error.Detail);
    }

    [Fact]
    public void NotFound_ShouldHaveCorrectMessage()
    {
        // Arrange & Act
        var error = BookErrors.NotFound;

        // Assert
        Assert.NotNull(error);
        Assert.Equal("Book not found", error.Detail);
    }
}