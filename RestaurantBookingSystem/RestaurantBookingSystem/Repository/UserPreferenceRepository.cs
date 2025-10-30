using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Model.Customers;

namespace RestaurantBookingSystem.Repository
{
    public class UserPreferenceRepository : IUserPreferenceRepository
    {
        private readonly BookingContext _context;

        public UserPreferenceRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<List<UserPreferenceDto>> GetAllUserPreferencesAsync()
        {
            return await _context.UserPreferences
                .AsNoTracking()
                .Select(pref => new UserPreferenceDto
                {
                    PreferenceId = pref.PreferenceId,
                    UserId = pref.UserId,
                    Theme = pref.Theme.ToString(),
                    NotificationsEnabled = pref.NotificationsEnabled,
                    PreferredCity = pref.PreferredCity,
                    PreferredFoodType = pref.PreferredFoodType
                }).ToListAsync();
        }

        public async Task<UserPreferenceDto?> GetUserPreferenceAsync(int userId)
        {
            var pref = await _context.UserPreferences
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (pref == null) return null;

            return new UserPreferenceDto
            {
                PreferenceId = pref.PreferenceId,
                UserId = pref.UserId,
                Theme = pref.Theme.ToString(),
                NotificationsEnabled = pref.NotificationsEnabled,
                PreferredCity = pref.PreferredCity,
                PreferredFoodType = pref.PreferredFoodType
            };
        }

        public async Task<bool> CreateUserPreferenceAsync(CreateUserPreferenceDto createDto)
        {
            var preference = new UserPreferences
            {
                UserId = createDto.UserId,
                Theme = !string.IsNullOrEmpty(createDto.Theme) ? Enum.Parse<UserTheme>(createDto.Theme, true) : UserTheme.Light,
                NotificationsEnabled = createDto.NotificationsEnabled,
                PreferredCity = createDto.PreferredCity,
                PreferredFoodType = createDto.PreferredFoodType
            };

            _context.UserPreferences.Add(preference);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserPreferenceAsync(int userId, UpdateUserPreferenceDto updateDto)
        {
            var pref = await _context.UserPreferences
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (pref == null) return false;

            if (updateDto.Theme != null)
                pref.Theme = Enum.Parse<UserTheme>(updateDto.Theme, true);

            if (updateDto.NotificationsEnabled.HasValue)
                pref.NotificationsEnabled = updateDto.NotificationsEnabled;

            if (!string.IsNullOrEmpty(updateDto.PreferredCity))
                pref.PreferredCity = updateDto.PreferredCity;

            if (!string.IsNullOrEmpty(updateDto.PreferredFoodType))
                pref.PreferredFoodType = updateDto.PreferredFoodType;

            _context.UserPreferences.Update(pref);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> DeleteUserPreferenceAsync(int userId)
        {
            var preference = await _context.UserPreferences.FirstOrDefaultAsync(p => p.UserId == userId);
            if (preference == null) return false;

            _context.UserPreferences.Remove(preference);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
