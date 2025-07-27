using Istijara.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istijara.Repository.Data.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(ua => ua.Id);
            builder.Property(ua => ua.AddressLine1).IsRequired().HasMaxLength(200);
            builder.Property(ua => ua.City).IsRequired().HasMaxLength(100);
            builder.Property(ua => ua.State).IsRequired().HasMaxLength(100);
            builder.Property(ua => ua.PostalCode).IsRequired().HasMaxLength(20);
            builder.Property(ua => ua.Country).IsRequired().HasMaxLength(100);
            builder.Property(ua => ua.Latitude).HasColumnType("decimal(18,2)");
            builder.Property(ua => ua.Longitude).HasColumnType("decimal(18,2)");
            builder.HasOne(ua => ua.UserProfile)
                .WithMany(u => u.Addresses)
                .HasForeignKey(ua => ua.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}