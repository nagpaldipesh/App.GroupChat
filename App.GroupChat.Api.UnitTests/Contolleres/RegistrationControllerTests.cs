using App.GroupChat.Api.Controllers;
using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Services;
using App.GroupChat.UnitTest.Common.Automapper;
using NSubstitute;
using NUnit.Framework;
using App.GroupChat.Services.Security;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.UnitTest.Common.Helpers;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace App.GroupChat.Api.UnitTests.Contolleres {
    [TestFixture]
    public class RegistrationControllerTests {
        private IGenericRepository<User> _userRepository;
        private RegistrationController _controller;

        [SetUp]
        public void Setup() {
            var mapper = AutomapperConfiguration.Configure();
            _userRepository = Substitute.For<IGenericRepository<User>>();
            var userService = new UserService(_userRepository, mapper, new PasswordHasher());
            _controller = new RegistrationController(userService);
        }
        [Test]
        public async Task AddUser_ShouldReturnTrue_WhenUsernameIsUnique() {
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(false);
            var userDto = UserHelper.GetUserDtos().First();
            var response = await _controller.AddUserAsync(userDto);
            
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();
        }

        [Test]
        public void AddUser_ShouldThrowError_WhenUserNameIsAlreadyTaken() {
            var userDto = UserHelper.GetUserDtos().First();
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            Assert.ThrowsAsync<DuplicateUsernameException>(() => _controller.AddUserAsync(userDto)).Message.ShouldBe("Username is already taken.");
        }
    }
}
