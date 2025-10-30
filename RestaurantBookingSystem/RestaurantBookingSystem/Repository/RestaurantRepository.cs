using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.Interface;
using RestaurantBookingSystem.Model.Restaurant;

namespace RestaurantBookingSystem.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly BookingContext _context;

        public RestaurantRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<Restaurants> CreateRestaurantAsync(Restaurants restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }
    }
}
