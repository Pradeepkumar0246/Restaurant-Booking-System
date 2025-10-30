using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Model.Customers;

namespace RestaurantBookingSystem.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly BookingContext _context;

        public WishlistRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<List<WishlistDto>> GetUserWishlistAsync(int userId)
        {
            return await _context.Wishlist
                .Include(w => w.User)
                .Where(w => w.UserId == userId)
                .Select(w => new WishlistDto
                {
                    WishlistId = w.WishlistId,
                    UserId = w.UserId,
                    ItemId = w.ItemId,
                    RestaurantId = w.RestaurantId,
                    CreatedAt = w.CreatedAt,
                    UserName = w.User != null ? $"{w.User.FirstName} {w.User.LastName}" : null
                }).ToListAsync();
        }

        public async Task<bool> AddToWishlistAsync(CreateWishlistDto createDto)
        {
            var wishlist = new Wishlist
            {
                UserId = createDto.UserId,
                ItemId = createDto.ItemId,
                RestaurantId = createDto.RestaurantId,
                CreatedAt = DateTime.Now
            };

            _context.Wishlist.Add(wishlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromWishlistAsync(int wishlistId)
        {
            var wishlist = await _context.Wishlist.FindAsync(wishlistId);
            if (wishlist == null) return false;

            _context.Wishlist.Remove(wishlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromWishlistAsync(int userId, int itemId)
        {
            var wishlist = await _context.Wishlist
                .Where(w => w.UserId == userId && w.ItemId == itemId)
                .FirstOrDefaultAsync();

            if (wishlist == null) return false;

            _context.Wishlist.Remove(wishlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int userId, int itemId)
        {
            return await _context.Wishlist
                .AnyAsync(w => w.UserId == userId && w.ItemId == itemId);
        }
    }
}