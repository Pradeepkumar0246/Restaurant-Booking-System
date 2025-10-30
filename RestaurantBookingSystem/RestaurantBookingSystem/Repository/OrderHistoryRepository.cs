using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IRepository;

namespace RestaurantBookingSystem.Repository
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly BookingContext _context;

        public OrderHistoryRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<List<OrderHistoryDto>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Select(o => new OrderHistoryDto
                {
                    OrderId = o.OrderId,
                    RestaurantId = o.RestaurantId,
                    UserId = o.UserId,
                    ItemsList = o.ItemsList,
                    OrderType = o.OrderType,
                    QtyOrdered = o.QtyOrdered,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    UserName = o.User != null ? $"{o.User.FirstName} {o.User.LastName}" : null
                }).ToListAsync();
        }

        public async Task<List<OrderHistoryDto>> GetOrdersByRestaurantAsync(int restaurantId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Where(o => o.RestaurantId == restaurantId)
                .Select(o => new OrderHistoryDto
                {
                    OrderId = o.OrderId,
                    RestaurantId = o.RestaurantId,
                    UserId = o.UserId,
                    ItemsList = o.ItemsList,
                    OrderType = o.OrderType,
                    QtyOrdered = o.QtyOrdered,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    UserName = o.User != null ? $"{o.User.FirstName} {o.User.LastName}" : null
                }).ToListAsync();
        }

        public async Task<List<OrderHistoryDto>> GetOrdersByUserAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .Select(o => new OrderHistoryDto
                {
                    OrderId = o.OrderId,
                    RestaurantId = o.RestaurantId,
                    UserId = o.UserId,
                    ItemsList = o.ItemsList,
                    OrderType = o.OrderType,
                    QtyOrdered = o.QtyOrdered,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    UserName = o.User != null ? $"{o.User.FirstName} {o.User.LastName}" : null
                }).ToListAsync();
        }

        public async Task<OrderHistoryDto?> GetOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Where(o => o.OrderId == orderId)
                .FirstOrDefaultAsync();

            if (order == null) return null;

            return new OrderHistoryDto
            {
                OrderId = order.OrderId,
                RestaurantId = order.RestaurantId,
                UserId = order.UserId,
                ItemsList = order.ItemsList,
                OrderType = order.OrderType,
                QtyOrdered = order.QtyOrdered,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                UserName = order.User != null ? $"{order.User.FirstName} {order.User.LastName}" : null
            };
        }
    }
}