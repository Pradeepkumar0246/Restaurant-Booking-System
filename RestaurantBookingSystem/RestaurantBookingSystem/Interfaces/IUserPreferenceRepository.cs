using RestaurantBookingSystem.DTO;

namespace RestaurantBookingSystem.Interface.IRepository
{
    public interface IUserPreferenceRepository
    {
        Task<List<UserPreferenceDto>> GetAllUserPreferencesAsync();
        Task<UserPreferenceDto?> GetUserPreferenceAsync(int userId);
        Task<bool> CreateUserPreferenceAsync(CreateUserPreferenceDto createDto);
        Task<bool> UpdateUserPreferenceAsync(int userId, UpdateUserPreferenceDto updateDto);
        Task<bool> DeleteUserPreferenceAsync(int userId);
    }
}
