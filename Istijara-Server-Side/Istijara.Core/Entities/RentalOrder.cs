namespace Istijara.Core.Entities
{
    public class RentalOrder : BaseEntity
    {
        public Guid RentalRequestId { get; set; }
        public Guid ItemId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BorrowerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SecurityDeposit { get; set; }
        public decimal DeliveryFee { get; set; } = 0.00m;
        public string DeliveryMethod { get; set; } = string.Empty;
        public string? DeliveryAddress { get; set; }
        public string Status { get; set; } = "Confirmed";
        public string PaymentStatus { get; set; } = "Pending";
        public string SecurityDepositStatus { get; set; } = "Held";
        public bool IsLate { get; set; } = false;
        public decimal LateFees { get; set; } = 0.00m;
        public DateTime? CompletedAt { get; set; }
        // Navigation properties
        public virtual RentalRequest RentalRequest { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
        public virtual UserProfile Owner { get; set; } = null!;
        public virtual UserProfile Borrower { get; set; } = null!;
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}