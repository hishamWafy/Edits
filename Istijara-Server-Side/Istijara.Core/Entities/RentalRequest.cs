namespace Istijara.Core.Entities
{
    public class RentalRequest : BaseEntity
    {
        public Guid ItemId { get; set; }
        public Guid BorrowerId { get; set; }
        public DateTime RequestedStartDate { get; set; }
        public DateTime RequestedEndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SecurityDeposit { get; set; }
        public decimal DeliveryFee { get; set; } = 0.00m;
        public string DeliveryMethod { get; set; } = "Pickup";
        public string? DeliveryAddress { get; set; }
        public string? Message { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ResponseAt { get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(2);
        // Computed property
        public int RequestedDays => (RequestedEndDate - RequestedStartDate).Days + 1;
        // Navigation properties
        public virtual Item Item { get; set; } = null!;
        public virtual UserProfile Borrower { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}