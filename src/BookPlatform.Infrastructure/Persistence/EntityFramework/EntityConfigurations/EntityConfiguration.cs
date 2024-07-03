using BookPlatform.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPlatform.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id).IsUnique();

        builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
    }
}