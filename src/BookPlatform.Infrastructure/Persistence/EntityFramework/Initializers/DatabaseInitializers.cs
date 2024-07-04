using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Infrastructure.Persistence.EntityFramework.Initializers;

public static class DatabaseInitializers
{
    public static Task InitializeFullTextSearchAsync(this DbContext context, CancellationToken cancellationToken = default)
    {
        return context.Database.ExecuteSqlRawAsync(@"
            IF NOT EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE name = 'ftCatalog')
            BEGIN
                CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;
            END
        ", cancellationToken);
    }
    
    public static Task InitializeFullTextSearchForBooksAsync(this DbContext context, CancellationToken cancellationToken = default)
    {
        return context.Database.ExecuteSqlRawAsync(@"
            CREATE FULLTEXT INDEX ON Books
            (
                Title LANGUAGE 1033,
                Description LANGUAGE 1033,
                Author LANGUAGE 1033,
                Genre LANGUAGE 1033,
                Isbn LANGUAGE 1033
            )
            KEY INDEX PK_Books
            ON ftCatalog
            WITH CHANGE_TRACKING AUTO;
        ", cancellationToken);
    }
}