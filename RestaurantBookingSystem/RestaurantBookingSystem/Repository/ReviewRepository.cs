using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.DTO;
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

        public async Task<List<ReviewDto>> GetAllReviewsAsync()
        {
            return await _context.Review
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    RestaurantId = r.RestaurantId,
                    Rating = r.Rating,
                    Comments = r.Comments,
                    ReviewDate = r.ReviewDate,
                    UserName = r.User != null ? $"{r.User.FirstName} {r.User.LastName}" : null,
                    RestaurantName = r.Restaurant != null ? r.Restaurant.RestaurantName : null
                }).ToListAsync();
        }

        public async Task<List<ReviewDto>> GetReviewsByRestaurantAsync(int restaurantId)
        {
            return await _context.Review
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .Where(r => r.RestaurantId == restaurantId)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    RestaurantId = r.RestaurantId,
                    Rating = r.Rating,
                    Comments = r.Comments,
                    ReviewDate = r.ReviewDate,
                    UserName = r.User != null ? $"{r.User.FirstName} {r.User.LastName}" : null,
                    RestaurantName = r.Restaurant != null ? r.Restaurant.RestaurantName : null
                }).ToListAsync();
        }

        public async Task<ReviewDto?> GetReviewAsync(int reviewId)
        {
            var review = await _context.Review
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .Where(r => r.ReviewId == reviewId)
                .FirstOrDefaultAsync();

            if (review == null) return null;

            return new ReviewDto
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                RestaurantId = review.RestaurantId,
                Rating = review.Rating,
                Comments = review.Comments,
                ReviewDate = review.ReviewDate,
                UserName = review.User != null ? $"{review.User.FirstName} {review.User.LastName}" : null,
                RestaurantName = review.Restaurant != null ? review.Restaurant.RestaurantName : null
            };
        }

        public async Task<bool> CreateReviewAsync(CreateReviewDto createDto)
        {
            var review = new Review
            {
                UserId = createDto.UserId,
                RestaurantId = createDto.RestaurantId,
                Rating = createDto.Rating,
                Comments = createDto.Comments,
                ReviewDate = DateTime.Now
            };

            _context.Review.Add(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Review.FindAsync(reviewId);
            if (review == null) return false;

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}