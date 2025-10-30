using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Model.Manager;

namespace RestaurantBookingSystem.Interface
{
    public interface IManagerRepository
    {
        Task<ManagerDetails> CreateManagerAsync(ManagerRegisterDTO dto, string passwordHash);
    }
}
