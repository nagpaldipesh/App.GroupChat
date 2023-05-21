using App.GroupChat.Api.Controllers;
using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Security;
using App.GroupChat.Services.Services;
using App.GroupChat.UnitTest.Common.Automapper;
using App.GroupChat.UnitTest.Common.Helpers;
using AutoMapper;
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
    public class EditUserControllerTests {
        private IGenericRepository<User> _userRepository;
        private EditUserController _controller;
        private IMapper _mapper;
        [SetUp]
        public void Setup() {
            _mapper = AutomapperConfiguration.Configure();
            _userRepository = Substitute.For<IGenericRepository<User>>();
            var userService = new UserService(_userRepository, _mapper, new PasswordHasher());
            _controller = new EditUserController(userService);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                       {
                            new Claim(ClaimTypes.Name, "dipeshnagpal"),
                            new Claim(ClaimTypes.Role, "1"),
                            new Claim("UserId", "12345"),
                       }, "mock"));

            _controller.ControllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
        [Test]
        public async Task EditUser_ShouldReturnTrue_WhenUsernameIsUnique() {
            var user = UserHelper.GetUsers().First();

            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user }.AsQueryable().BuildMock());
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(false);
            var userDto = UserHelper.GetUserDtos().First();
            var response = await _controller.EditUserAsync(userDto);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkResult>();
        }
        [Test]
        public void EditUser_ShouldThrowNotFoundException_WhenUserIsNotFound() {
            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User>().AsQueryable().BuildMock());
            var userDto = UserHelper.GetUserDtos().First();
            Assert.ThrowsAsync<NotFoundException>(() => _controller.EditUserAsync(userDto)).Message.ShouldBe("User not found.");
        }
        [Test]
        public void EditUser_ShouldThrowDuplicateUsernameException_WhenUserNameIsAlreadyTaken() {
            var user = UserHelper.GetUsers().First();

            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user }.AsQueryable().BuildMock());
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            var userDto = UserHelper.GetUserDtos().First();
            Assert.ThrowsAsync<DuplicateUsernameException>(() => _controller.EditUserAsync(userDto)).Message.ShouldBe("Username is already taken.");
        }
    }
}
