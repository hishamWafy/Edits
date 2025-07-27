namespace Istijara.Core.Entities
{
    public class BlockedUser : BaseEntity
    {
        public Guid BlockerId { get; set; }
        public Guid BlockedUserId { get; set; }
        public string? Reason { get; set; }
        public virtual UserProfile Blocker { get; set; } = null!;
        public virtual UserProfile BlockedUserProfile { get; set; } = null!;
    }
}