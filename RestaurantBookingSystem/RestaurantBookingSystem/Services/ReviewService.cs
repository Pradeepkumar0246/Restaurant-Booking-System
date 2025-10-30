using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Interface.IService;
using RestaurantBookingSystem.Model.Customers;

namespace RestaurantBookingSystem.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewService(IReviewRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        public async Task<List<ReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepo.GetAllReviewsAsync();
            return reviews.Select(r => new ReviewDto
            {
                ReviewId = r.ReviewId,
                UserId = r.UserId,
                RestaurantId = r.RestaurantId,
                Rating = r.Rating,
                Comments = r.Comments,
                ReviewDate = r.ReviewDate,
                UserName = r.User != null ? $"{r.User.FirstName} {r.User.LastName}" : null,
                RestaurantName = r.Restaurant != null ? r.Restaurant.RestaurantName : null
            }).ToList();
        }

        public async Task<List<ReviewDto>> GetReviewsByRestaurantAsync(int restaurantId)
        {
            if (restaurantId <= 0)
                throw new ArgumentException("Restaurant ID must be greater than 0");

            var reviews = await _reviewRepo.GetReviewsByRestaurantAsync(restaurantId);
            return reviews.Select(r => new ReviewDto
            {
                ReviewId = r.ReviewId,
                UserId = r.UserId,
                RestaurantId = r.RestaurantId,
                Rating = r.Rating,
                Comments = r.Comments,
                ReviewDate = r.ReviewDate,
                UserName = r.User != null ? $"{r.User.FirstName} {r.User.LastName}" : null,
                RestaurantName = r.Restaurant != null ? r.Restaurant.RestaurantName : null
            }).ToList();
        }

        public async Task<ReviewDto?> GetReviewAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Review ID must be greater than 0");

            var review = await _reviewRepo.GetReviewAsync(reviewId);
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
            if (createDto.UserId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            if (createDto.RestaurantId <= 0)
                throw new ArgumentException("Restaurant ID must be greater than 0");

            if (createDto.Rating < 0 || createDto.Rating > 10)
                throw new ArgumentException("Rating must be between 0 and 10");

            if (string.IsNullOrWhiteSpace(createDto.Comments))
                throw new ArgumentException("Comments are required");

            var review = new Review
            {
                UserId = createDto.UserId,
                RestaurantId = createDto.RestaurantId,
                Rating = createDto.Rating,
                Comments = createDto.Comments,
                ReviewDate = DateTime.Now
            };

            await _reviewRepo.CreateReviewAsync(review);
            return true;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Review ID must be greater than 0");

            return await _reviewRepo.DeleteReviewAsync(reviewId);
        }
    }
}