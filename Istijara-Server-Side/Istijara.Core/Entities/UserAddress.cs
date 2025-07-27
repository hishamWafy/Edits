namespace Istijara.Core.Entities
{
    public class UserAddress : BaseEntity
    {
        public Guid UserProfileId { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsDefault { get; set; } = false;
        public string AddressType { get; set; } = "Home";
        public virtual UserProfile UserProfile { get; set; } = null!;
    }
}