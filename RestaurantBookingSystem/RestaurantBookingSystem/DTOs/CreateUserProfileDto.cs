using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingSystem.DTO
{
    public class CreateUserProfileDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Phone]
        public string? Mobile { get; set; }
    }
}