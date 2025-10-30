using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface;
using RestaurantBookingSystem.Model.Manager;
using RestaurantBookingSystem.Model.Restaurant;
using System;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantBookingSystem.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly BookingContext _context;

        public ManagerService(IManagerRepository managerRepository, IRestaurantRepository restaurantRepository, BookingContext context)
        {
            _managerRepository = managerRepository;
            _restaurantRepository = restaurantRepository;
            _context = context;
        }

        public async Task<(ManagerDetails Manager, Restaurants Restaurant)> RegisterManagerWithRestaurantAsync(ManagerRegisterDTO dto)
        {
            // 1. Check if user password matches manager password
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.UserId);
            if (user != null && user.Password == dto.Password)
            {
                throw new Exception("Manager password cannot be same as user password.");
            }

            // 2. Hash manager password
            string passwordHash = HashPassword(dto.Password);

            // 3. Create Manager
            var manager = await _managerRepository.CreateManagerAsync(dto, passwordHash);

            // 4. Create Restaurant
            var restaurant = await _restaurantRepository.CreateRestaurantAsync(dto.Restaurant, manager.ManagerId);

            return (manager, restaurant);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
