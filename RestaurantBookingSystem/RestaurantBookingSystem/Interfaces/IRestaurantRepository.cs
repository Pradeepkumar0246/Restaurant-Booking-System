using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Model.Restaurant;

namespace RestaurantBookingSystem.Interface
{
    public interface IRestaurantRepository
    {
        Task<Restaurants> CreateRestaurantAsync(RestaurantCreateDTO dto, int managerId);
    }
}
