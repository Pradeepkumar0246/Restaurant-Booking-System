using RestaurantBookingSystem.Model.Restaurant;

namespace RestaurantBookingSystem.Interface
{
    public interface IRestaurantRepository
    {
        Task<Restaurants> CreateRestaurantAsync(Restaurants restaurant);
    }
}
