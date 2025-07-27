using Istijara.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istijara.Repository.Data.Configurations
{
    public class RentalOrderConfiguration : IEntityTypeConfiguration<RentalOrder>
    {
        public void Configure(EntityTypeBuilder<RentalOrder> builder)
        {
            builder.HasKey(ro => ro.Id);
            builder.HasOne(ro => ro.RentalRequest)
                .WithMany()
                .HasForeignKey(ro => ro.RentalRequestId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ro => ro.Item)
                .WithMany()
                .HasForeignKey(ro => ro.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ro => ro.Owner)
                .WithMany(u => u.RentalOrdersAsOwner)
                .HasForeignKey(ro => ro.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ro => ro.Borrower)
                .WithMany(u => u.RentalOrdersAsBorrower)
                .HasForeignKey(ro => ro.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(ro => ro.Ratings)
                .WithOne(r => r.RentalOrder)
                .HasForeignKey(r => r.RentalOrderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(ro => ro.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(ro => ro.SecurityDeposit).HasColumnType("decimal(18,2)");
            builder.Property(ro => ro.DeliveryFee).HasColumnType("decimal(18,2)");
            builder.Property(ro => ro.LateFees).HasColumnType("decimal(18,2)");
        }
    }
}
