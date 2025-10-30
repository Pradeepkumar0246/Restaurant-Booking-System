using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Model.Customers;

namespace RestaurantBookingSystem.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly BookingContext _context;

        public UserProfileRepository(BookingContext context)
        {
            _context = context;
        }
        public async Task<List<UserProfileDto>> GetAllUserProfilesAsync()
        {
            return await _context.Users
                .Select(u => new UserProfileDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Mobile = u.Mobile,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLogin = u.LastLogin
                }).ToListAsync();
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            var user = await _context.Set<Users>()
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();

            if (user == null) return null;

            return new UserProfileDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin
            };
        }

        public async Task<bool> CreateUserProfileAsync(CreateUserProfileDto createDto)
        {
            var user = new Users
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                Password = createDto.Password,
                Mobile = createDto.Mobile,
                IsActive = true,
                CreatedAt = DateTime.Now,
                LastLogin = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserProfileAsync(int userId, UpdateUserProfileDto updateDto)
        {
            var user = await _context.Set<Users>().FindAsync(userId);
            if (user == null) return false;

            user.FirstName = updateDto.FirstName ?? user.FirstName;
            user.LastName = updateDto.LastName ?? user.LastName;
            user.Email = updateDto.Email ?? user.Email;
            user.Mobile = updateDto.Mobile ?? user.Mobile;

            _context.Set<Users>().Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserProfileAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
