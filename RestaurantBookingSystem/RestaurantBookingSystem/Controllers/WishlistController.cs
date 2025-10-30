using Microsoft.AspNetCore.Mvc;
using RestaurantBookingSystem.DTO;
using RestaurantBookingSystem.Interface.IService;

namespace RestaurantBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserWishlist(int userId)
        {
            try
            {
                var wishlist = await _wishlistService.GetUserWishlistAsync(userId);
                return Ok(wishlist);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] CreateWishlistDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _wishlistService.AddToWishlistAsync(createDto);
                if (result)
                    return Ok(new { Message = "Item added to wishlist successfully" });

                return BadRequest(new { Message = "Failed to add item to wishlist" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
        }

        [HttpDelete("{wishlistId}")]
        public async Task<IActionResult> RemoveFromWishlist(int wishlistId)
        {
            try
            {
                var result = await _wishlistService.RemoveFromWishlistAsync(wishlistId);
                if (!result)
                    return NotFound(new { Message = "Wishlist item not found" });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("user/{userId}/item/{itemId}")]
        public async Task<IActionResult> RemoveFromWishlist(int userId, int itemId)
        {
            try
            {
                var result = await _wishlistService.RemoveFromWishlistAsync(userId, itemId);
                if (!result)
                    return NotFound(new { Message = "Wishlist item not found" });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}