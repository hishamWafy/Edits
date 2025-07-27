namespace Istijara.Core.Entities
{
    public class UserSettings : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Language { get; set; } = "en";
        public bool NotificationsEnabled { get; set; } = true;
        public bool PushNotificationsEnabled { get; set; } = true;
        public bool EmailNotificationsEnabled { get; set; } = true;
        public bool SMSNotificationsEnabled { get; set; } = false;
        public string Theme { get; set; } = "Light";
        public string Currency { get; set; } = "USD";
        public virtual UserProfile User { get; set; } = null!;
    }
}