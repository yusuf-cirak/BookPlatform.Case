namespace BookPlatform.Domain;

public class BookNote : AuditEntity
{
    public string BookId { get; set; }
    public string Note { get; set; } = string.Empty;

    public ShareType ShareType { get; set; } = ShareType.Private;


    public virtual Book Book { get; set; }
    
    
    public static BookNote Create(string bookId, string note)
    {
        return new BookNote
        {
            BookId = bookId,
            Note = note
        };
    }


    public static BookNote Create(string bookId, string note, ShareType shareType)
    {
        return new BookNote
        {
            BookId = bookId,
            Note = note,
            ShareType = shareType
        };
    }

    public void Update(string note)
    {
        Note = note;
    }
    
    public void UpdateShareType(ShareType shareType)
    {
        ShareType = shareType;
    }
}

public enum ShareType
{
    Public,
    OnlyFriends,
    Private
}