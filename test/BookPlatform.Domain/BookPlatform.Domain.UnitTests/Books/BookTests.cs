using BookPlatform.Domain.ValueObjects;

namespace BookPlatform.Domain.UnitTests.Books;

public class BookTests
{
    [Fact]
    public void Create_ShouldReturnBookWithCorrectProperties()
    {
        // Arrange
        var title = "Test Title";
        var author = "Test Author";
        var isbn = "1234567890";
        var pages = 300;
        var genre = "Test Genre";
        var publishedDate = new DateTime(2020, 1, 1);
        var description = "Test Description";

        // Act
        var book = Book.Create(title, author, isbn, pages, genre, publishedDate, description);

        // Assert
        Assert.NotNull(book);
        Assert.Equal(title, book.Title);
        Assert.Equal(author, book.Author);
        Assert.Equal(isbn, book.Isbn);
        Assert.Equal(pages, book.Pages);
        Assert.Equal(genre, book.Genre);
        Assert.Equal(publishedDate, book.PublishedDate);
        Assert.Equal(description, book.Description);
    }

    [Fact]
    public void Create_WithShelfLocation_ShouldReturnBookWithCorrectProperties()
    {
        // Arrange
        var title = "Test Title";
        var author = "Test Author";
        var isbn = "1234567890";
        var pages = 300;
        var genre = "Test Genre";
        var publishedDate = new DateTime(2020, 1, 1);
        var description = "Test Description";
        var shelfLocation = ShelfLocation.Create("Room1", "SectionA", "Shelf2", "Position3");

        // Act
        var book = Book.Create(title, author, isbn, pages, genre, publishedDate, description, shelfLocation);

        // Assert
        Assert.NotNull(book);
        Assert.Equal(title, book.Title);
        Assert.Equal(author, book.Author);
        Assert.Equal(isbn, book.Isbn);
        Assert.Equal(pages, book.Pages);
        Assert.Equal(genre, book.Genre);
        Assert.Equal(publishedDate, book.PublishedDate);
        Assert.Equal(description, book.Description);
        Assert.Equal(shelfLocation, book.ShelfLocation);
    }

    [Fact]
    public void Create_WithShelfLocationDetails_ShouldReturnBookWithCorrectProperties()
    {
        // Arrange
        var title = "Test Title";
        var author = "Test Author";
        var isbn = "1234567890";
        var pages = 300;
        var genre = "Test Genre";
        var publishedDate = new DateTime(2020, 1, 1);
        var description = "Test Description";
        var room = "Room1";
        var section = "SectionA";
        var shelf = "Shelf2";
        var position = "Position3";

        // Act
        var book = Book.Create(title, author, isbn, pages, genre, publishedDate, description, room, section, shelf,
            position);

        // Assert
        Assert.NotNull(book);
        Assert.Equal(title, book.Title);
        Assert.Equal(author, book.Author);
        Assert.Equal(isbn, book.Isbn);
        Assert.Equal(pages, book.Pages);
        Assert.Equal(genre, book.Genre);
        Assert.Equal(publishedDate, book.PublishedDate);
        Assert.Equal(description, book.Description);
        Assert.NotNull(book.ShelfLocation);
        Assert.Equal(room, book.ShelfLocation.Room);
        Assert.Equal(section, book.ShelfLocation.Section);
        Assert.Equal(shelf, book.ShelfLocation.Shelf);
        Assert.Equal(position, book.ShelfLocation.Position);
    }

    [Fact]
    public void Update_ShouldUpdateBookProperties()
    {
        // Arrange
        var book = new Book("Old Title", "Old Author", "1234567890", 200, "Old Genre", new DateTime(2010, 1, 1),
            "Old Description");
        var newTitle = "New Title";
        var newAuthor = "New Author";
        var newIsbn = "0987654321";
        var newPages = 400;
        var newGenre = "New Genre";
        var newPublishedDate = new DateTime(2020, 1, 1);
        var newDescription = "New Description";

        // Act
        book.Update(newTitle, newAuthor, newIsbn, newPages, newGenre, newPublishedDate, newDescription);

        // Assert
        Assert.Equal(newTitle, book.Title);
        Assert.Equal(newAuthor, book.Author);
        Assert.Equal(newIsbn, book.Isbn);
        Assert.Equal(newPages, book.Pages);
        Assert.Equal(newGenre, book.Genre);
        Assert.Equal(newPublishedDate, book.PublishedDate);
        Assert.Equal(newDescription, book.Description);
    }

    [Fact]
    public void AssignToShelf_ShouldAssignShelfLocation()
    {
        // Arrange
        var book = new Book("Title", "Author", "1234567890", 300, "Genre", new DateTime(2020, 1, 1), "Description");
        var shelfLocation = ShelfLocation.Create("Room1", "SectionA", "Shelf2", "Position3");

        // Act
        book.AssignToShelf(shelfLocation);

        // Assert
        Assert.Equal(shelfLocation, book.ShelfLocation);
    }

    [Fact]
    public void RemoveFromShelf_ShouldSetShelfLocationToNull()
    {
        // Arrange
        var shelfLocation = ShelfLocation.Create("Room1", "SectionA", "Shelf2", "Position3");
        var book = new Book("Title", "Author", "1234567890", 300, "Genre", new DateTime(2020, 1, 1), "Description")
        {
            ShelfLocation = shelfLocation
        };

        // Act
        book.RemoveFromShelf();

        // Assert
        Assert.Null(book.ShelfLocation);
    }
}