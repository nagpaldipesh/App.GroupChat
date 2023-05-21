using App.GroupChat.Api.Auth;
using App.GroupChat.Api.Auth.Interfaces;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GroupChat.Api.IntegrationTests.Auth {
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

        [Test]
        public void GenerateTokenTest() {
            int roleId = (int)App.GroupData.Shared.UserRoles.SuperAdmin;

            var x = _tokenService.GenerateToken("dipeshnagpal", roleId);

        }

    }
}
