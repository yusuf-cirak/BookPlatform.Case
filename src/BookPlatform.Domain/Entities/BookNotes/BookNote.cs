using BookPlatform.Domain.Enums;

namespace BookPlatform.Domain;

public class BookNote : AuditEntity
{
    public string UserId { get; set; }
    public string BookId { get; set; }
    public string Note { get; set; } = string.Empty;

    public ShareType ShareType { get; set; } = ShareType.Private;


    public virtual User User { get; set; }
    public virtual Book Book { get; set; }


    public static BookNote Create(string bookId, string note)
    {
        return new BookNote
        {
            BookId = bookId,
            Note = note
        };
    }


    public static BookNote Create(string userId,string bookId, string note, ShareType shareType)
    {
        return new BookNote
        {
            UserId = userId,
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