namespace RestaurantBookingSystem.DTO
{
    public class ManagerRegisterDTO
    {
        public string ManagerName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Restaurant details (nested DTO)
        public RestaurantCreateDTO Restaurant { get; set; } = new RestaurantCreateDTO();
    }
}
