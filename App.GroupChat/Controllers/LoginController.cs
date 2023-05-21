using App.GroupChat.Api.Auth.Interfaces;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.GroupChat.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        private readonly ITokenService _tokenService;
        private readonly ILoginService _loginService;
        public LoginController(ITokenService tokenService, ILoginService loginService) {
            _tokenService = tokenService;
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userDto) {
            var user = await _loginService.ValidateUserCreds(userDto);
            return Ok(_tokenService.GenerateToken(user.Username, user.UserRoleId, user.UserId));
        }
    }
}
