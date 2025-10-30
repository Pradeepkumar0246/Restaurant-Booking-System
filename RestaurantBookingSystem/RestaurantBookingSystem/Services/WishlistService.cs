using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Interface.IService;

namespace RestaurantBookingSystem.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepo;

        public WishlistService(IWishlistRepository wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }

        public async Task<List<WishlistDto>> GetUserWishlistAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            return await _wishlistRepo.GetUserWishlistAsync(userId);
        }

        public async Task<bool> AddToWishlistAsync(CreateWishlistDto createDto)
        {
            if (createDto.UserId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            if (createDto.ItemId <= 0)
                throw new ArgumentException("Item ID must be greater than 0");

            if (createDto.RestaurantId <= 0)
                throw new ArgumentException("Restaurant ID must be greater than 0");

            // Check for duplicates
            var exists = await _wishlistRepo.ExistsAsync(createDto.UserId, createDto.ItemId);
            if (exists)
                throw new InvalidOperationException("Item already exists in wishlist");

            return await _wishlistRepo.AddToWishlistAsync(createDto);
        }

        public async Task<bool> RemoveFromWishlistAsync(int wishlistId)
        {
            if (wishlistId <= 0)
                throw new ArgumentException("Wishlist ID must be greater than 0");

            return await _wishlistRepo.RemoveFromWishlistAsync(wishlistId);
        }

        public async Task<bool> RemoveFromWishlistAsync(int userId, int itemId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            if (itemId <= 0)
                throw new ArgumentException("Item ID must be greater than 0");

            return await _wishlistRepo.RemoveFromWishlistAsync(userId, itemId);
        }
    }
}