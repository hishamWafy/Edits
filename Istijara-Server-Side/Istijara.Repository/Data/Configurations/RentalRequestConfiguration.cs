using Istijara.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istijara.Repository.Data.Configurations
{
    public class RentalRequestConfiguration : IEntityTypeConfiguration<RentalRequest>
    {
        public void Configure(EntityTypeBuilder<RentalRequest> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.Item)
                .WithMany()
                .HasForeignKey(r => r.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Borrower)
                .WithMany(u => u.RentalRequestsAsBorrower)
                .HasForeignKey(r => r.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(r => r.Messages)
                .WithOne(m => m.RentalRequest)
                .HasForeignKey(m => m.RentalRequestId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(r => r.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(r => r.SecurityDeposit).HasColumnType("decimal(18,2)");
            builder.Property(r => r.DeliveryFee).HasColumnType("decimal(18,2)");
        }
    }
}