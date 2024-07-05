using YC.Result;

namespace BookPlatform.Domain;

public static class BookNoteErrors
{
    public static readonly Error FailedToCreateBookNote = Error.Create("Failed to create book note");

    public static readonly Error FailedToDeleteBookNote = Error.Create("Failed to create book note");

    public static readonly Error NotFound = Error.Create("Book note is not found");

    public static readonly Error NotOwnerOfBookNote = Error.Create("","You are not the owner of this book note",401);
    
    public static readonly Error NotVisible = Error.Create("","You are not allowed to view this book note",401);

    public static readonly Error NotFriendOfOwnerBookNote =
        Error.Create("","You are not friend of owner of this book note",401);
    
    public static readonly Error IsPrivate =
        Error.Create("","This book note is private and only owner can view it",401);
}