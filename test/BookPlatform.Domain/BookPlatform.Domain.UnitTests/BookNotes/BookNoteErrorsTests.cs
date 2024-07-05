namespace BookPlatform.Domain.UnitTests.BookNotes;

public class BookNoteErrorsTests
    {
        [Fact]
        public void FailedToCreateBookNote_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var error = BookNoteErrors.FailedToCreateBookNote;

            // Assert
            Assert.NotNull(error);
            Assert.Equal("Failed to create book note", error.Detail);
        }

        [Fact]
        public void FailedToDeleteBookNote_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var error = BookNoteErrors.FailedToDeleteBookNote;

            // Assert
            Assert.NotNull(error);
            Assert.Equal("Failed to create book note", error.Detail);
        }

        [Fact]
        public void NotFound_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var error = BookNoteErrors.NotFound;

            // Assert
            Assert.NotNull(error);
            Assert.Equal("Book note is not found", error.Detail);
        }

        [Fact]
        public void NotOwnerOfBookNote_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var error = BookNoteErrors.NotOwnerOfBookNote;

            // Assert
            Assert.NotNull(error);
            Assert.Equal("You are not the owner of this book note", error.Detail);
        }

        [Fact]
        public void NotFriendOfOwnerBookNote_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var error = BookNoteErrors.NotFriendOfOwnerBookNote;

            // Assert
            Assert.NotNull(error);
            Assert.Equal("You are not friend of owner of this book note", error.Detail);
        }

        [Fact]
        public void IsPrivate_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var error = BookNoteErrors.IsPrivate;

            // Assert
            Assert.NotNull(error);
            Assert.Equal("This book note is private and only owner can view it", error.Detail);
        }
    }