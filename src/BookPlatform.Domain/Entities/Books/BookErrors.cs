using YC.Result;

namespace BookPlatform.Domain;

public static class BookErrors
{
    public static readonly Error FailedToCreate = Error.Create("Failed to create book");
    public static readonly Error FailedToUpdate = Error.Create("Failed to update book");
    
    public static readonly Error FailedToDelete = Error.Create("Failed to delete book");
    
    public static readonly Error NotFound = Error.Create("Book not found");
}