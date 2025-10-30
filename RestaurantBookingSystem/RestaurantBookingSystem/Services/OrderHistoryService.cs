using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;
using RestaurantBookingSystem.Interface.IService;

namespace RestaurantBookingSystem.Services
{
    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly IOrderHistoryRepository _orderRepo;

        public OrderHistoryService(IOrderHistoryRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<List<OrderHistoryDto>> GetAllOrdersAsync()
        {
            return await _orderRepo.GetAllOrdersAsync();
        }

        public async Task<List<OrderHistoryDto>> GetOrdersByRestaurantAsync(int restaurantId)
        {
            if (restaurantId <= 0)
                throw new ArgumentException("Restaurant ID must be greater than 0");

            return await _orderRepo.GetOrdersByRestaurantAsync(restaurantId);
        }

        public async Task<List<OrderHistoryDto>> GetOrdersByUserAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0");

            return await _orderRepo.GetOrdersByUserAsync(userId);
        }

        public async Task<OrderHistoryDto?> GetOrderAsync(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentException("Order ID must be greater than 0");

            return await _orderRepo.GetOrderAsync(orderId);
        }
    }
}