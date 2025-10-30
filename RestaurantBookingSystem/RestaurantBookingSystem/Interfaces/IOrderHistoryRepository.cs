using RestaurantBookingSystem.DTO;

namespace RestaurantBookingSystem.Interface.IRepository
{
    public interface IOrderHistoryRepository
    {
        Task<List<OrderHistoryDto>> GetAllOrdersAsync();
        Task<List<OrderHistoryDto>> GetOrdersByRestaurantAsync(int restaurantId);
        Task<List<OrderHistoryDto>> GetOrdersByUserAsync(int userId);
        Task<OrderHistoryDto?> GetOrderAsync(int orderId);
    }
}