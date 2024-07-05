namespace BookPlatform.WebAPI.IntegrationTests.Fixtures;

public sealed class BookNoteFixture : BaseFixture
{
    public string BookNoteId { get; set; }
}

[CollectionDefinition("BookNoteCollection")]
public sealed class BookNoteCollectionFixture : ICollectionFixture<BookNoteFixture>;