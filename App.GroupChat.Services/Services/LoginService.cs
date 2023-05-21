using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Security;
using App.GroupChat.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.GroupChat.Services.Services {
    public class LoginService : ILoginService {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public LoginService(IGenericRepository<User> userRepository, IPasswordHasher passwordHasher, IMapper mapper) {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<UserDto> ValidateUserCreds(UserLoginDto userLoginDto) {
            var hashPassword = _passwordHasher.GetHashPassword(userLoginDto.Password);
            var user = await _userRepository.FindBy(cond => cond.Username == userLoginDto.Username.ToLower()
            && cond.UserPassword.Password == hashPassword).FirstOrDefaultAsync();

            if (user != null) {
                return _mapper.Map<UserDto>(user);
            }

            throw new InvalidCredsException("Invalid User Details");
        }
    }
}
