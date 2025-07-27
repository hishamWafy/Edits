namespace Istijara.Core.Entities
{
    public class ItemImage : BaseEntity
    {
        public Guid ItemId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; } = false;
        public int DisplayOrder { get; set; } = 0;
        public virtual Item Item { get; set; } = null!;
    }
}