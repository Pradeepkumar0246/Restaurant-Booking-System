using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Model.Manager;
using RestaurantBookingSystem.Model.Restaurant;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantBookingSystem.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserProfileRepository _userRepository;

        public ManagerService(IManagerRepository managerRepository, IRestaurantRepository restaurantRepository, IUserProfileRepository userRepository)
        {
            _managerRepository = managerRepository;
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }

        public async Task<(ManagerDetails Manager, Restaurants Restaurant)> RegisterManagerWithRestaurantAsync(ManagerRegisterDTO dto)
        {
            // 1. Check if user password matches manager password
            var user = await _userRepository.GetUserAsync(dto.UserId);
            if (user != null && user.Password == dto.Password)
            {
                throw new Exception("Manager password cannot be same as user password.");
            }

            // 2. Hash manager password
            string passwordHash = HashPassword(dto.Password);

            // 3. Create Manager Model
            var manager = new ManagerDetails
            {
                ManagerName = dto.ManagerName,
                UserId = dto.UserId,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            var createdManager = await _managerRepository.CreateManagerAsync(manager);

            // 4. Create Restaurant Model
            var restaurant = new Restaurants
            {
                RestaurantName = dto.Restaurant.RestaurantName,
                Description = dto.Restaurant.Description,
                Location = dto.Restaurant.Location,
                City = dto.Restaurant.City,
                ContactNo = dto.Restaurant.ContactNo,
                DeliveryCharge = dto.Restaurant.DeliveryCharge,
                RestaurantCategory = dto.Restaurant.RestaurantCategory,
                RestaurantType = dto.Restaurant.RestaurantType,
                ManagerId = createdManager.ManagerId,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var createdRestaurant = await _restaurantRepository.CreateRestaurantAsync(restaurant);

            return (createdManager, createdRestaurant);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
