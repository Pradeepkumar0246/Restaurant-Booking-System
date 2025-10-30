using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.Interface;
using RestaurantBookingSystem.Model.Manager;

namespace RestaurantBookingSystem.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly BookingContext _context;

        public ManagerRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<ManagerDetails> CreateManagerAsync(ManagerDetails manager)
        {
            _context.ManagerDetails.Add(manager);
            await _context.SaveChangesAsync();
            return manager;
        }
    }
}
