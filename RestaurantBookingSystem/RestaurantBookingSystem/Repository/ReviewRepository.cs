using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Model.Customers;

namespace RestaurantBookingSystem.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookingContext _context;

        public ReviewRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _context.Review
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsByRestaurantAsync(int restaurantId)
        {
            return await _context.Review
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .Where(r => r.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Review?> GetReviewAsync(int reviewId)
        {
            return await _context.Review
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(r => r.ReviewId == reviewId);
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            _context.Review.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Review.FindAsync(reviewId);
            if (review == null) return false;

            _context.Review.Remove(review);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}