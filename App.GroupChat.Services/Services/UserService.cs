using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Security;
using App.GroupChat.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.GroupChat.Services.Services {
    public class UserService : IUserService {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IGenericRepository<User> userRepository, IMapper mapper, IPasswordHasher passwordHasher) {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> AddUserAsync(UserDto userDto) {
            bool isUserTaken = await _userRepository.AnyAsync(cond => cond.Username == userDto.Username.ToLower());
            if (isUserTaken) {
                throw new DuplicateUsernameException("Username is already taken.");
            }
            var user = _mapper.Map<User>(userDto);
            var hashPassword = _passwordHasher.GetHashPassword(userDto.Password);

            user.UserPassword = new UserPassword() {
                Password = hashPassword
            };

            _userRepository.Add(user);
            await _userRepository.SaveAsync();

            return true;
        }
        public async Task<bool> EditUserAsync(UserProfileDto userProfileDto, long userId) {
            var user = await _userRepository.FindBy(cond => cond.UserId == userId).FirstOrDefaultAsync();
            if (user != null) {
                bool isUserTaken = await _userRepository.AnyAsync(cond => cond.Username == userProfileDto.Username.ToLower() && cond.UserId != userId);
                if (isUserTaken) {
                    throw new DuplicateUsernameException("Username is already taken.");
                }
                user.FirstName = userProfileDto.FirstName;
                user.LastName = userProfileDto.LastName;
                user.Email = userProfileDto.Email;
                user.Username = userProfileDto.Username;
                _userRepository.Update(user);
                await _userRepository.SaveAsync();
            }
            else {
                throw new NotFoundException("User not found.");
            }
            return true;
        }

        public async Task<UserProfileDto> GetUserByIdAsync(long userId) {
            var user = await _userRepository.FindBy(cond => cond.UserId == userId).FirstOrDefaultAsync();
            if (user == null) {
                throw new NotFoundException("User not found");
            }
            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task<ICollection<UserProfileDto>> GetUsersAsync() {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<ICollection<UserProfileDto>>(users);
        }
    }
}
