namespace Istijara.Core.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? RelatedEntityType { get; set; }
        public Guid? RelatedEntityId { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsPush { get; set; } = true;
        public DateTime? ReadAt { get; set; }
        public virtual UserProfile User { get; set; } = null!;
    }
}