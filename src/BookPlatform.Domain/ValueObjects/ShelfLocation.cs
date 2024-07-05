namespace BookPlatform.Domain.ValueObjects;

public sealed record ShelfLocation
{
    public string Room { get; set; }

    public string Section { get; set; }

    public string Shelf { get; set; }

    public string Position { get; set; }

    public ShelfLocation()
    {
        
    }

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