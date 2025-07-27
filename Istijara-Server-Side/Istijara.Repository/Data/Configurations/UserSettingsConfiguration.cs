using Istijara.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istijara.Repository.Data.Configurations
{
    public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
    {
        public void Configure(EntityTypeBuilder<UserSettings> builder)
        {
            builder.HasKey(us => us.Id);
            builder.Property(us => us.Language).IsRequired().HasMaxLength(10);
            builder.Property(us => us.Theme).IsRequired().HasMaxLength(20);
            builder.Property(us => us.Currency).IsRequired().HasMaxLength(10);
            builder.HasOne(us => us.User)
                .WithOne(u => u.Settings)
                .HasForeignKey<UserSettings>(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}