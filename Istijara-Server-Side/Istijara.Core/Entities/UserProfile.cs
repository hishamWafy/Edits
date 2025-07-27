namespace Istijara.Core.Entities
{
    public class UserProfile : BaseEntity
    {
        public string UserId { get; set; } = string.Empty; // Identity User ID
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsVerified { get; set; } = false;
        public string? IdentityDocumentUrl { get; set; }
        public int TotalRatingsAsOwner { get; set; } = 0;
        public decimal AverageRatingAsOwner { get; set; } = 0.00m;
        public int TotalRatingsAsBorrower { get; set; } = 0;
        public decimal AverageRatingAsBorrower { get; set; } = 0.00m;
        public bool IsActive { get; set; } = true;
        // Navigation properties (add these later)
        public virtual ICollection<UserAddress> Addresses { get; set; } = new List<UserAddress>();
        public virtual ICollection<Item> OwnedItems { get; set; } = new List<Item>();
        public virtual UserSettings? Settings { get; set; }
        public virtual ICollection<RentalRequest> RentalRequestsAsBorrower { get; set; } = new List<RentalRequest>();
        public virtual ICollection<RentalOrder> RentalOrdersAsOwner { get; set; } = new List<RentalOrder>();
        public virtual ICollection<RentalOrder> RentalOrdersAsBorrower { get; set; } = new List<RentalOrder>();
        public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<Rating> RatingsGiven { get; set; } = new List<Rating>();
        public virtual ICollection<Rating> RatingsReceived { get; set; } = new List<Rating>();
        public virtual ICollection<BlockedUser> BlockedUsers { get; set; } = new List<BlockedUser>();
        public virtual ICollection<BlockedUser> BlockedByUsers { get; set; } = new List<BlockedUser>();
        public virtual ICollection<UserActivityHistory> ActivityHistories { get; set; } = new List<UserActivityHistory>();
    }
}