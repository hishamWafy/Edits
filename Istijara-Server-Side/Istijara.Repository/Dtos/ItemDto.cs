using System.ComponentModel.DataAnnotations;

namespace Istijara.Repository.Dtos
{
    public class ItemDto
    {
       
        public string CategoryName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DailyRentalPrice { get; set; }
        public decimal SecurityDeposit { get; set; } = 0.00m;
        public int MinRentalPeriod { get; set; } = 1;
        public int MaxRentalPeriod { get; set; } = 30;
        public string Condition { get; set; } = "Good";
        public string Location { get; set; } = string.Empty;
        public string DeliveryOptions { get; set; } = "Pickup";
        public int? DeliveryRadius { get; set; }
        public decimal DeliveryFee { get; set; } = 0.00m;




    }
}
