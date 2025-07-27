using Istijara.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istijara.Repository.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(200);
            builder.HasIndex(i => i.Name);
            builder.Property(i => i.Description).HasMaxLength(1000);
            builder.Property(i => i.DailyRentalPrice).HasColumnType("decimal(18,2)");
            builder.Property(i => i.SecurityDeposit).HasColumnType("decimal(18,2)");
            builder.Property(i => i.DeliveryFee).HasColumnType("decimal(18,2)");
            builder.Property(i => i.AverageRating).HasColumnType("decimal(18,2)");
            builder.HasOne(i => i.Owner)
                .WithMany(u => u.OwnedItems)
                .HasForeignKey(i => i.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(i => i.Images)
                .WithOne(img => img.Item)
                .HasForeignKey(img => img.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}