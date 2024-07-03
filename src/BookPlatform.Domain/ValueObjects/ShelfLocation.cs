namespace BookPlatform.Domain.ValueObjects;

public sealed record ShelfLocation
{
    public required string Room { get; set; }

    public required string Section { get; set; }

    public required string Shelf { get; set; }

    public required string Position { get; set; }

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