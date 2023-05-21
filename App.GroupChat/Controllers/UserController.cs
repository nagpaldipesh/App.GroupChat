using App.GroupChat.Api.Filters;
using App.GroupChat.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.GroupChat.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        [AdminAuthFilter]
        public async Task<IActionResult> GetUsersAsync() {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }
        [HttpGet("Details")]
        public async Task<IActionResult> GetUserDetailsByIdAsync() {
            var userId = Convert.ToInt64(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }
    }
}
