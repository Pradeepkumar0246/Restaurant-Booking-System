using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface;
using RestaurantBookingSystem.Model.Restaurant;
using System;

namespace RestaurantBookingSystem.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly BookingContext _context;

        public RestaurantRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<Restaurants> CreateRestaurantAsync(RestaurantCreateDTO dto, int managerId)
        {
            var restaurant = new Restaurants
            {
                RestaurantName = dto.RestaurantName,
                Description = dto.Description,
                Location = dto.Location,
                City = dto.City,
                ContactNo = dto.ContactNo,
                DeliveryCharge = dto.DeliveryCharge,
                RestaurantCategory = dto.RestaurantCategory,
                RestaurantType = dto.RestaurantType,
                ManagerId = managerId,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }
    }
}
