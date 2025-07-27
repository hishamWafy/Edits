namespace Istijara.Core.Entities
{
    public class Message : BaseEntity
    {
        public Guid RentalRequestId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string MessageContent { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; }
        public virtual RentalRequest RentalRequest { get; set; } = null!;
        public virtual UserProfile Sender { get; set; } = null!;
        public virtual UserProfile Receiver { get; set; } = null!;
    }
}