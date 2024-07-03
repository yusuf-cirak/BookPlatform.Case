using BookPlatform.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPlatform.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

public sealed class UserFriendConfiguration : IEntityTypeConfiguration<UserFriend>
{
    public void Configure(EntityTypeBuilder<UserFriend> builder)
    {
        builder.ToTable("UserFriends");

        builder.Property(r => r.UserId).HasColumnName("UserId");
        builder.Property(r => r.FriendUserId).HasColumnName("FriendUserId");

        builder.HasKey(r => new { r.UserId, r.FriendUserId });
        builder.HasIndex(r => new { r.UserId, r.FriendUserId }).IsUnique();

        // Defining the relationships explicitly to avoid conflicts
        builder.HasOne(r => r.User)
            .WithMany(u => u.UserFriends)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasOne(r => r.FriendUser)
            .WithMany() // Leaving empty to avoid conflicts
            .HasForeignKey(r => r.FriendUserId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        // Remove unique indexes on single columns if they are not needed
        builder.HasIndex(r => r.UserId).IsUnique(false);
        builder.HasIndex(r => r.FriendUserId).IsUnique(false);
    }
}