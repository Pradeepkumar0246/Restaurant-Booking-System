using RestaurantBookingSystem.DTO;

namespace RestaurantBookingSystem.Interface.IRepository
{
    public interface IWishlistRepository
    {
        Task<List<WishlistDto>> GetUserWishlistAsync(int userId);
        Task<bool> AddToWishlistAsync(CreateWishlistDto createDto);
        Task<bool> RemoveFromWishlistAsync(int wishlistId);
        Task<bool> RemoveFromWishlistAsync(int userId, int itemId);
        Task<bool> ExistsAsync(int userId, int itemId);
    }
}