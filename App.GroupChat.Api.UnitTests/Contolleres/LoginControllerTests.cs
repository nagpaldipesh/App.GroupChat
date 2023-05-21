using App.GroupChat.Api.Auth;
using App.GroupChat.Api.Auth.Interfaces;
using App.GroupChat.Api.Controllers;
using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Security;
using App.GroupChat.Services.Services;
using App.GroupChat.UnitTest.Common.Automapper;
using App.GroupChat.UnitTest.Common.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MockQueryable.Moq;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Linq.Expressions;

namespace App.GroupChat.Api.UnitTests.Contolleres {
    [TestFixture]
    public class LoginControllerTests {
        private ITokenService _tokenService;
        private IMapper _mapper;
        private IGenericRepository<User> _userRepository;
        private LoginController _controller;

        [SetUp]
        public void Setup() {
            var configuration = Substitute.For<IConfiguration>();
            configuration[Arg.Any<string>()].Returns("fjboJU3s7rw2Oafzum5fBxZoZ5jihQRbpBZcxZFd/gY=");
            _tokenService = new TokenService(configuration);
            _mapper = AutomapperConfiguration.Configure();
            _userRepository = Substitute.For<IGenericRepository<User>>();
            var loginService = new LoginService(_userRepository, new PasswordHasher(), _mapper);
            _controller = new LoginController(_tokenService, loginService);
        }

        [Test]
        public void LoginAsync_ShouldThrowsInvalidCredsException_WhenUserCredsAreInvalid() {
            UserLoginDto userLoginDto = new UserLoginDto() { Username = "dipeshnagpal", Password = "Password" };
            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User>().AsQueryable().BuildMock());
            Assert.ThrowsAsync<InvalidCredsException>(() => _controller.LoginAsync(userLoginDto));
        }

        [Test]
        public async Task LoginAsync_ShouldReturnUserDto_WhenUserCredsAreValid() {
            UserLoginDto userLoginDto = new UserLoginDto() { Username = "dipeshnagpal", Password = "Password" };
            var userDto = UserHelper.GetUserDtos().First();

            List<User> users = new List<User>() { _mapper.Map<User>(userDto) };

            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(users.AsQueryable().BuildMock());

            var response = await _controller.LoginAsync(userLoginDto);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();
        }
    }
}
