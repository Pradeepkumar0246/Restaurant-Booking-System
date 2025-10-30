using RestaurantBookingSystem.DTO;

namespace RestaurantBookingSystem.Interface.IService
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetAllReviewsAsync();
        Task<List<ReviewDto>> GetReviewsByRestaurantAsync(int restaurantId);
        Task<ReviewDto?> GetReviewAsync(int reviewId);
        Task<bool> CreateReviewAsync(CreateReviewDto createDto);
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}