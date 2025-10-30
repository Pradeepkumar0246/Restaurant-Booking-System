using RestaurantBookingSystem.DTO;

namespace RestaurantBookingSystem.Interface.IRepository
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfileDto>> GetAllUserProfilesAsync();
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        Task<bool> CreateUserProfileAsync(CreateUserProfileDto createDto);
        Task<bool> UpdateUserProfileAsync(int userId, UpdateUserProfileDto updateDto);
        Task<bool> DeleteUserProfileAsync(int userId);
    }
}
