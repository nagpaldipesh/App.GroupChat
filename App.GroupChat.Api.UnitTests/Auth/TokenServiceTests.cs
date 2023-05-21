using App.GroupChat.Api.Auth.Interfaces;
using App.GroupChat.Api.Auth;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NSubstitute;
using Newtonsoft.Json.Linq;
using Shouldly;
using App.GroupData.Shared;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace App.GroupChat.Api.UnitTests.Auth {
    [TestFixture]
    public class TokenServiceTests {
        private IConfiguration _configuration;
        private ITokenService _tokenService;

        [SetUp]
        public void Setup() {
            _configuration = Substitute.For<IConfiguration>();
            _configuration[Arg.Any<string>()].Returns("fjboJU3s7rw2Oafzum5fBxZoZ5jihQRbpBZcxZFd/gY=");
            _tokenService = new TokenService(_configuration);
        }

        //[Test]
        //public async Task GenerateToken_CreatingValidTokens() {
        //    int roleId = (int)UserRoles.SuperAdmin;

        //    var token = _tokenService.GenerateToken("dipeshnagpal", roleId);
        //    token.ShouldBeOfType<string>(token);

        //    //var principal = await _tokenService.AuthenticateJwtToken(token);

        //    //principal.ShouldNotBeNull();


        //    //token.ShouldBeOfType<IPrincipal>(principal);
        //}
    }
}
