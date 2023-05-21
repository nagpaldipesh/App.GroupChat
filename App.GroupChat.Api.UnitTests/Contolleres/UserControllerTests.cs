using App.GroupChat.Api.Controllers;
using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Security;
using App.GroupChat.Services.Services;
using App.GroupChat.UnitTest.Common.Automapper;
using App.GroupChat.UnitTest.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Linq.Expressions;
using System.Security.Claims;

namespace App.GroupChat.Api.UnitTests.Contolleres {
    [TestFixture]
    public class UserControllerTests {
        private IGenericRepository<User> _userRepository;
        private UserController _controller;

        [SetUp]
        public void Setup() {
            var mapper = AutomapperConfiguration.Configure();
            _userRepository = Substitute.For<IGenericRepository<User>>();
            var userService = new UserService(_userRepository, mapper, new PasswordHasher());

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, "dipeshnagpal"),
                            new Claim(ClaimTypes.Role, "1"),
                            new Claim("UserId", "12345"),
                        }, "mock"));

            _controller = new UserController(userService);
            _controller.ControllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
        [Test]
        public async Task GetUsers_ShouldReturnUserDetails() {
            var users = UserHelper.GetUsers();

            _userRepository.GetAllAsync().Returns(users);

            var response = await _controller.GetUsersAsync();
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();
            var value = (ICollection<UserProfileDto>)result.Value;
            value.Count.ShouldBe(3);
        }
        [Test]
        public async Task GetUserDetailsByIdAsync_ShouldReturnUserDetails() {
            var user = UserHelper.GetUsers().First();

            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user }.AsQueryable().BuildMock());

            var response = await _controller.GetUserDetailsByIdAsync();
            response.ShouldNotBeNull();
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();

            var value = (UserProfileDto)result.Value;
            value.FirstName.ShouldBe(user.FirstName);
            value.LastName.ShouldBe(user.LastName);
            value.Username.ShouldBe(user.Username);
            value.Email.ShouldBe(user.Email);
        }
        [Test]
        public void GetUserDetailsByIdAsync_ShouldThrowNotFoundException_WhenUserIsNotFound() {
            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User>().AsQueryable().BuildMock());
            var userDto = UserHelper.GetUserDtos().First();
            Assert.ThrowsAsync<NotFoundException>(() => _controller.GetUserDetailsByIdAsync()).Message.ShouldBe("User not found");
        }
    }
}
