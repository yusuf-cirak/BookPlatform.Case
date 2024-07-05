using BookPlatform.Domain.ValueObjects;

namespace BookPlatform.Domain;

public class Book : AuditEntity
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Isbn { get; private set; }
    public int Pages { get; private set; }
    public string Genre { get; private set; }
    public DateTime PublishedDate { get; private set; }
    public string Description { get; private set; }
    public virtual ShelfLocation ShelfLocation { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    
    public Book(string id,string title, string author, string isbn, int pages, string genre, DateTime publishedDate,
        string description,ShelfLocation shelfLocation)
    {
        Id = id;
        Title = title;
        Author = author;
        Isbn = isbn;
        Pages = pages;
        Genre = genre;
        PublishedDate = publishedDate;
        Description = description;
        ShelfLocation = shelfLocation;
    }

    public Book(string title, string author, string isbn, int pages, string genre, DateTime publishedDate,
        string description)
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        Pages = pages;
        Genre = genre;
        PublishedDate = publishedDate;
        Description = description;
    }

    public void Update(string title, string author, string isbn, int pages, string genre, DateTime publishedDate,
        string description)
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        Pages = pages;
        Genre = genre;
        PublishedDate = publishedDate;
        Description = description;
    }

    public void AssignToShelf(ShelfLocation shelfLocation)
    {
        ShelfLocation = shelfLocation;
    }

    public void RemoveFromShelf()
    {
        ShelfLocation = null;
    }
    
    public static Book Create(string id,string title, string author, string isbn, int pages, string genre, DateTime publishedDate,
        string description,ShelfLocation shelfLocation)
    {
        return new Book(id,title, author, isbn, pages, genre, publishedDate, description,shelfLocation);
    }

    public static Book Create(string title, string author, string isbn, int pages, string genre, DateTime publishedDate,
        string description)
    {
        return new Book(title, author, isbn, pages, genre, publishedDate, description);
    }

    public static Book Create(string title, string author, string isbn, int pages, string genre, DateTime publishedDate,
        string description, ShelfLocation shelfLocation)
    {
        return new Book(title, author, isbn, pages, genre, publishedDate, description)
        {
            ShelfLocation = shelfLocation
        };
    }

    public static Book Create(string title, string author, string isbn, int pages, string genre, DateTime publishedDate,
        string description, string room, string section, string shelf, string position)
    {
        return new Book(title, author, isbn, pages, genre, publishedDate, description)
        {
            ShelfLocation = ShelfLocation.Create(room, section, shelf, position)
        };
    }
}