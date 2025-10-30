using RestaurantBookingSystem.Model.Customers;

namespace RestaurantBookingSystem.Interface.IRepository
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllReviewsAsync();
        Task<List<Review>> GetReviewsByRestaurantAsync(int restaurantId);
        Task<Review?> GetReviewAsync(int reviewId);
        Task<Review> CreateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}