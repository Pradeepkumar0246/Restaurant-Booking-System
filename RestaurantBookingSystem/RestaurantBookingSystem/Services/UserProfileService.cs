using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Interface.IService;
using RestaurantBookingSystem.Repository;

namespace RestaurantBookingSystem.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userRepo;

        public UserProfileService(IUserProfileRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<List<UserProfileDto>> GetAllProfilesAsync()
        {
            return await _userRepo.GetAllUserProfilesAsync();
        }

        public async Task<UserProfileDto?> GetProfileAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            return await _userRepo.GetUserProfileAsync(userId);
        }

        public async Task<bool> CreateProfileAsync(CreateUserProfileDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.FirstName))
                throw new ArgumentException("First name is required");

            if (string.IsNullOrWhiteSpace(createDto.LastName))
                throw new ArgumentException("Last name is required");

            if (string.IsNullOrWhiteSpace(createDto.Email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(createDto.Password))
                throw new ArgumentException("Password is required");

            return await _userRepo.CreateUserProfileAsync(createDto);
        }

        public async Task<bool> UpdateProfileAsync(int userId, UpdateUserProfileDto updateDto)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            return await _userRepo.UpdateUserProfileAsync(userId, updateDto);
        }

        public async Task<bool> DeleteProfileAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            return await _userRepo.DeleteUserProfileAsync(userId);
        }
    }
}
