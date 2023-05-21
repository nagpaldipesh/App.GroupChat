using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Security;
using App.GroupChat.Services.Services;
using App.GroupChat.UnitTest.Common.Automapper;
using App.GroupChat.UnitTest.Common.Helpers;
using AutoMapper;
using MockQueryable.Moq;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Linq.Expressions;

namespace App.GroupChat.Services.UnitTests.Services {
    [TestFixture]
    public class UserServiceTests {
        private IGenericRepository<User> _userRepository;
        private IMapper _mapper;
        private UserService _userService;

        [SetUp]
        public void Setup() {
            _mapper = AutomapperConfiguration.Configure();
            _userRepository = Substitute.For<IGenericRepository<User>>();
            _userService = new UserService(_userRepository, _mapper, new PasswordHasher());
        }
        [Test]
        public async Task AddUser_ShouldReturnTrue_WhenUsernameIsUnique() {
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(false);
            var userDto = UserHelper.GetUserDtos().First();
            var response = await _userService.AddUserAsync(userDto);
            response.ShouldBe(true);
        }

        [Test]
        public void AddUser_ShouldThrowError_WhenUserNameIsAlreadyTaken() {
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            Assert.ThrowsAsync<DuplicateUsernameException>(() => _userService.AddUserAsync(new UserDto())).Message.ShouldBe("Username is already taken.");
        }
        [Test]
        public async Task EditUser_ShouldReturnTrue_WhenUsernameIsUnique() {
            var user = UserHelper.GetUsers().First();

            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user}.AsQueryable().BuildMock());
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(false);
            var userDto = UserHelper.GetUserDtos().First();
            var response = await _userService.EditUserAsync(userDto, user.UserId);
            response.ShouldBe(true);
        }
        [Test]
        public void EditUser_ShouldThrowNotFoundException_WhenUserIsNotFound() { 
            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User>().AsQueryable().BuildMock());
            var userDto = UserHelper.GetUserDtos().First();
            Assert.ThrowsAsync<NotFoundException>(() => _userService.EditUserAsync(userDto, userDto.UserId)).Message.ShouldBe("User not found.");
        }
        [Test]
        public void EditUser_ShouldThrowDuplicateUsernameException_WhenUserNameIsAlreadyTaken() {
            var user = UserHelper.GetUsers().First();

            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user }.AsQueryable().BuildMock());
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            var userDto = UserHelper.GetUserDtos().First();
            Assert.ThrowsAsync<DuplicateUsernameException>(() => _userService.EditUserAsync(userDto, userDto.UserId)).Message.ShouldBe("Username is already taken.");
        }
        [Test]
        public async Task GetUserById_ShouldReturnUserDetails() {
            var user = UserHelper.GetUsers().First();

            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user }.AsQueryable().BuildMock());
            
            var response = await _userService.GetUserByIdAsync(user.UserId);
            response.ShouldNotBeNull();
            response.FirstName.ShouldBe(user.FirstName);
            response.LastName.ShouldBe(user.LastName);
            response.Username.ShouldBe(user.Username);
            response.Email.ShouldBe(user.Email);
        }
        [Test]
        public void GetUserById_ShouldThrowNotFoundException_WhenUserIsNotFound() {
            _userRepository.FindBy(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User>().AsQueryable().BuildMock());
            var userDto = UserHelper.GetUserDtos().First();
            Assert.ThrowsAsync<NotFoundException>(() => _userService.GetUserByIdAsync(userDto.UserId)).Message.ShouldBe("User not found");
        }
        [Test]
        public async Task GetUsers_ShouldReturnUserDetails() {
            var users = UserHelper.GetUsers();

            _userRepository.GetAllAsync().Returns(users);

            var response = await _userService.GetUsersAsync();
            response.ShouldNotBeNull();
            response.Count.ShouldBe(3);
        }

    }
}
