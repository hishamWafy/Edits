namespace Istijara.Core.Entities
{
    public class Rating : BaseEntity
    {
        public Guid RentalOrderId { get; set; }
        public Guid RaterId { get; set; }
        public Guid RatedUserId { get; set; }
        public Guid ItemId { get; set; }
        public string RatingType { get; set; } = string.Empty; // OwnerRating, BorrowerRating, ItemRating
        public int RatingValue { get; set; }
        public string? Review { get; set; }
        public virtual RentalOrder RentalOrder { get; set; } = null!;
        public virtual UserProfile Rater { get; set; } = null!;
        public virtual UserProfile RatedUser { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
    }
}