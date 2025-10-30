using RestaurantBookingSystem.Model.Manager;

namespace RestaurantBookingSystem.Interface
{
    public interface IManagerRepository
    {
        Task<ManagerDetails> CreateManagerAsync(ManagerDetails manager);
    }
}
