using Istijara.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istijara.Repository.Data.Configurations
{
    public class BlockedUserConfiguration : IEntityTypeConfiguration<BlockedUser>
    {
        public void Configure(EntityTypeBuilder<BlockedUser> builder)
        {
            builder.HasKey(bu => bu.Id);
            builder.HasOne(bu => bu.Blocker)
                .WithMany(u => u.BlockedUsers)
                .HasForeignKey(bu => bu.BlockerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(bu => bu.BlockedUserProfile)
                .WithMany(u => u.BlockedByUsers)
                .HasForeignKey(bu => bu.BlockedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}