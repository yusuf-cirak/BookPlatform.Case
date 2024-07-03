namespace BookPlatform.Domain;

public sealed class ShelfLocation : AuditEntity
{
    public required string Room { get; set; }

    public required string Section { get; set; }

    public required string Shelf { get; set; }

    public required string Position { get; set; }

    public ICollection<Book> Books { get; set; } = new HashSet<Book>();

    public static ShelfLocation Create(string room, string section, string shelf, string position)
    {
        return new ShelfLocation
        {
            Room = room,
            Section = section,
            Shelf = shelf,
            Position = position
        };
    }
}