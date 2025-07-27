namespace Istijara.Core.Entities
{
    public class UserActivityHistory : BaseEntity
    {
        public Guid UserId { get; set; }
        public string ActivityType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? RelatedEntityType { get; set; }
        public Guid? RelatedEntityId { get; set; }
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public virtual UserProfile User { get; set; } = null!;
    }
}