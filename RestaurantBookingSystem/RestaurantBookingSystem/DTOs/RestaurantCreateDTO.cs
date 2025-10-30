namespace RestaurantBookingSystem.DTO
{
    public class RestaurantCreateDTO
    {
        public string RestaurantName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public decimal? DeliveryCharge { get; set; }
        public FoodType RestaurantType { get; set; } = FoodType.Both;
        public RestaurantCategory RestaurantCategory { get; set; } = RestaurantCategory.Restaurant;
    }
}
