using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IService;

namespace RestaurantBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferenceController : ControllerBase
    {
        private readonly IUserPreferenceService _userPreferenceService;

        public UserPreferenceController(IUserPreferenceService userPreferenceService)
        {
            _userPreferenceService = userPreferenceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPreferences()
        {
            var preferences = await _userPreferenceService.GetAllPreferencesAsync();
            return Ok(preferences);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPreference(int userId)
        {
            try
            {
                var preference = await _userPreferenceService.GetPreferenceAsync(userId);
                if (preference == null)
                    return NotFound(new { Message = "Preferences not found" });

                return Ok(preference);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePreference([FromBody] CreateUserPreferenceDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _userPreferenceService.CreatePreferenceAsync(createDto);
                if (result)
                    return CreatedAtAction(nameof(GetPreference), new { userId = createDto.UserId }, new { Message = "Preference created successfully" });

                return BadRequest(new { Message = "Failed to create preference" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdatePreference(int userId, [FromBody] UpdateUserPreferenceDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _userPreferenceService.UpdatePreferenceAsync(userId, updateDto);
                if (!result)
                    return NotFound(new { Message = "Preferences not found or update failed" });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeletePreference(int userId)
        {
            try
            {
                var result = await _userPreferenceService.DeletePreferenceAsync(userId);
                if (!result)
                    return NotFound(new { Message = "Preferences not found" });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
