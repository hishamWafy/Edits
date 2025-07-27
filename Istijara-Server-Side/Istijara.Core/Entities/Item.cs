namespace Istijara.Core.Entities
{
    public class Item : BaseEntity
    {
        public Guid OwnerId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DailyRentalPrice { get; set; }
        public decimal SecurityDeposit { get; set; } = 0.00m;
        public int MinRentalPeriod { get; set; } = 1;
        public int MaxRentalPeriod { get; set; } = 30;
        public bool IsAvailable { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public string Condition { get; set; } = "Good";
        public string Location { get; set; } = string.Empty;
        public string DeliveryOptions { get; set; } = "Pickup";
        public int? DeliveryRadius { get; set; }
        public decimal DeliveryFee { get; set; } = 0.00m;
        public int TotalRatings { get; set; } = 0;
        public decimal AverageRating { get; set; } = 0.00m;
        public int TotalRentals { get; set; } = 0;
        // Navigation properties
        public virtual UserProfile Owner { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<ItemImage> Images { get; set; } = new List<ItemImage>();
    }
}