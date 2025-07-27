using Istijara.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istijara.Repository.Data.Configurations
{
    public class UserActivityHistoryConfiguration : IEntityTypeConfiguration<UserActivityHistory>
    {
        public void Configure(EntityTypeBuilder<UserActivityHistory> builder)
        {
            builder.HasKey(ua => ua.Id);
            builder.Property(ua => ua.ActivityType).IsRequired().HasMaxLength(100);
            builder.Property(ua => ua.Description).HasMaxLength(1000);
            builder.HasOne(ua => ua.User)
                .WithMany(u => u.ActivityHistories)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}