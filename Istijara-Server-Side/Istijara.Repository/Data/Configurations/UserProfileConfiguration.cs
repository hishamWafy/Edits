using Istijara.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istijara.Repository.Data.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.UserId).IsRequired();
            builder.HasIndex(u => u.UserId).IsUnique();
            builder.Property(u => u.AverageRatingAsOwner).HasColumnType("decimal(18,2)");
            builder.Property(u => u.AverageRatingAsBorrower).HasColumnType("decimal(18,2)");

            builder.HasMany(u => u.Addresses)
                .WithOne(a => a.UserProfile)
                .HasForeignKey(a => a.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.OwnedItems)
                .WithOne(i => i.Owner)
                .HasForeignKey(i => i.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Settings)
                .WithOne(s => s.User)
                .HasForeignKey<UserSettings>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}