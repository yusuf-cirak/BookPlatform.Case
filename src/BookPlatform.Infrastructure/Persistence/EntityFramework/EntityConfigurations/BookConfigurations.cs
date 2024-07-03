using BookPlatform.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPlatform.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

public class BookConfiguration : AuditableEntityConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder.ComplexProperty(b => b.ShelfLocation);
    }
}