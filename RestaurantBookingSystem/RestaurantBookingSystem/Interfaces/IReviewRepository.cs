using RestaurantBookingSystem.DTO;

namespace RestaurantBookingSystem.Interface.IRepository
{
    public interface IReviewRepository
    {
        Task<List<ReviewDto>> GetAllReviewsAsync();
        Task<List<ReviewDto>> GetReviewsByRestaurantAsync(int restaurantId);
        Task<ReviewDto?> GetReviewAsync(int reviewId);
        Task<bool> CreateReviewAsync(CreateReviewDto createDto);
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}