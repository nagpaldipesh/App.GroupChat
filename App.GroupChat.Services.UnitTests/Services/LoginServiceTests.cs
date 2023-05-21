using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Security;
using App.GroupChat.Services.Services;
using App.GroupChat.UnitTest.Common.Automapper;
using App.GroupChat.UnitTest.Common.Helpers;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using MockQueryable.Moq;
using Shouldly;
using App.GroupChat.Services.Exceptions;

namespace App.GroupChat.Services.UnitTests.Services {
    [TestFixture]
    public class LoginServiceTests {
        private IMapper _mapper;
        private Mock<IGenericRepository<User>> _userRepository;
        private LoginService _loginService;

        [SetUp]
        public void Setup() {
            _mapper = AutomapperConfiguration.Configure();
            _userRepository = new Mock<IGenericRepository<User>>();
            _loginService = new LoginService(_userRepository.Object, new PasswordHasher(), _mapper);
        }

        [Test]
        public async Task ValidateUserCreds_ShouldReturnUserDto_WhenUserCredsAreValid() {
            UserLoginDto userLoginDto = new UserLoginDto() { Username="dipeshnagpal", Password = "Password" };
            var userDto = UserHelper.GetUserDtos().First();

            List<User> users = new List<User>() { _mapper.Map<User>(userDto) };

            _userRepository.Setup(s => s.FindBy(It.IsAny<Expression<Func<User, bool>>>())).Returns(users.AsQueryable().BuildMock());

            var response = await _loginService.ValidateUserCreds(userLoginDto);
            response.ShouldNotBeNull();
            response.FirstName.ShouldBe(userDto.FirstName);
            response.LastName.ShouldBe(userDto.LastName);
            response.Email.ShouldBe(userDto.Email);
            response.Username.ShouldBe(userDto.Username);

        }
        
        [Test]
        public void ValidateUserCreds_ShouldThrowError_WhenCredsAreInvalid() {
            UserLoginDto userLoginDto = new UserLoginDto() { Username = "dipeshnagpal", Password = "Password" };
            _userRepository.Setup(s => s.FindBy(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User>().AsQueryable().BuildMock());
            Assert.ThrowsAsync<InvalidCredsException>(() => _loginService.ValidateUserCreds(userLoginDto));
        }

    }
}
