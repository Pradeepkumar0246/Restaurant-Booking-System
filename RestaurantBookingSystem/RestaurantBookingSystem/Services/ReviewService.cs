using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Interface.IService;

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
            return await _reviewRepo.GetAllReviewsAsync();
        }

        public async Task<List<ReviewDto>> GetReviewsByRestaurantAsync(int restaurantId)
        {
            if (restaurantId <= 0)
                throw new ArgumentException("Restaurant ID must be greater than 0");

            return await _reviewRepo.GetReviewsByRestaurantAsync(restaurantId);
        }

        public async Task<ReviewDto?> GetReviewAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Review ID must be greater than 0");

            return await _reviewRepo.GetReviewAsync(reviewId);
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

            return await _reviewRepo.CreateReviewAsync(createDto);
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Review ID must be greater than 0");

            return await _reviewRepo.DeleteReviewAsync(reviewId);
        }
    }
}