using Istijara.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Istijara.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<RentalRequest> RentalRequests { get; set; }
        public DbSet<RentalOrder> RentalOrders { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<BlockedUser> BlockedUsers { get; set; }
        public DbSet<UserActivityHistory> UserActivityHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
