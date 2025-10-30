using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface;
using RestaurantBookingSystem.Model.Manager;
using System;

namespace RestaurantBookingSystem.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly BookingContext _context;

        public ManagerRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<ManagerDetails> CreateManagerAsync(ManagerRegisterDTO dto, string passwordHash)
        {
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

            _context.ManagerDetails.Add(manager);
            await _context.SaveChangesAsync();
            return manager;
        }
    }
}
