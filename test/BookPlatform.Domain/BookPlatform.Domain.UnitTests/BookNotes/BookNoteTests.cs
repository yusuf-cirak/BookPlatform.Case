using BookPlatform.Domain.Enums;

namespace BookPlatform.Domain.UnitTests.BookNotes;

public class BookNoteTests
    {
        [Fact]
        public void Create_WithBookIdAndNote_ShouldReturnBookNote()
        {
            // Arrange
            var bookId = "book123";
            var note = "This is a test note.";

            // Act
            var bookNote = BookNote.Create(bookId, note);

            // Assert
            Assert.NotNull(bookNote);
            Assert.Equal(bookId, bookNote.BookId);
            Assert.Equal(note, bookNote.Note);
            Assert.Equal(ShareType.Private, bookNote.ShareType);
        }

        [Fact]
        public void Create_WithAllParameters_ShouldReturnBookNote()
        {
            // Arrange
            var userId = "user123";
            var bookId = "book123";
            var note = "This is a test note.";
            var shareType = ShareType.Public;

            // Act
            var bookNote = BookNote.Create(userId, bookId, note, shareType);

            // Assert
            Assert.NotNull(bookNote);
            Assert.Equal(userId, bookNote.UserId);
            Assert.Equal(bookId, bookNote.BookId);
            Assert.Equal(note, bookNote.Note);
            Assert.Equal(shareType, bookNote.ShareType);
        }

        [Fact]
        public void Update_ShouldUpdateNote()
        {
            // Arrange
            var bookNote = new BookNote
            {
                Note = "Old note."
            };
            var newNote = "New note.";

            // Act
            bookNote.Update(newNote);

            // Assert
            Assert.Equal(newNote, bookNote.Note);
        }

        [Fact]
        public void UpdateShareType_ShouldUpdateShareType()
        {
            // Arrange
            var bookNote = new BookNote
            {
                ShareType = ShareType.Private
            };
            var newShareType = ShareType.Public;

            // Act
            bookNote.UpdateShareType(newShareType);

            // Assert
            Assert.Equal(newShareType, bookNote.ShareType);
        }
    }