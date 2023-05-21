using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.GroupChat.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase {
        private readonly IUserService _userService;
        public RegistrationController(IUserService userService) {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("User")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserDto userDto) {
            var isUserCreated = await _userService.AddUserAsync(userDto);
            return Ok(isUserCreated);
        }
    }
}
