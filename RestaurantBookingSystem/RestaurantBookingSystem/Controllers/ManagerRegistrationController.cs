using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface;

namespace RestaurantBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerRegistrationController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerRegistrationController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost("RegisterManagerWithRestaurant")]
        public async Task<IActionResult> RegisterManagerWithRestaurant([FromBody] ManagerRegisterDTO dto)
        {
            try
            {
                var result = await _managerService.RegisterManagerWithRestaurantAsync(dto);
                return Ok(new
                {
                    Message = "Manager and Restaurant registered successfully.",
                    Manager = result.Manager,
                    Restaurant = result.Restaurant
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
