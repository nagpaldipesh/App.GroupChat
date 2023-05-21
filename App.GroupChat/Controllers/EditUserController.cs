using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.GroupChat.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EditUserController : ControllerBase {
        private readonly IUserService _userService;
        public EditUserController(IUserService userService) {
            _userService = userService;
        }

        [HttpPut("User")]
        public async Task<IActionResult> EditUserAsync([FromBody] UserProfileDto userDto) {
            var userId = Convert.ToInt64(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
            await _userService.EditUserAsync(userDto, userId);
            return Ok();
        }
    }
}
