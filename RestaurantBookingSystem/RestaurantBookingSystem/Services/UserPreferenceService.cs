using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Interface.IService;

namespace RestaurantBookingSystem.Services
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly IUserPreferenceRepository _repository;

        public UserPreferenceService(IUserPreferenceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserPreferenceDto>> GetAllPreferencesAsync()
        {
            return await _repository.GetAllUserPreferencesAsync();
        }

        public async Task<UserPreferenceDto?> GetPreferenceAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            return await _repository.GetUserPreferenceAsync(userId);
        }

        public async Task<bool> CreatePreferenceAsync(CreateUserPreferenceDto createDto)
        {
            if (createDto.UserId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            return await _repository.CreateUserPreferenceAsync(createDto);
        }

        public async Task<bool> UpdatePreferenceAsync(int userId, UpdateUserPreferenceDto updateDto)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            return await _repository.UpdateUserPreferenceAsync(userId, updateDto);
        }

        public async Task<bool> DeletePreferenceAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            return await _repository.DeleteUserPreferenceAsync(userId);
        }
    }
}
