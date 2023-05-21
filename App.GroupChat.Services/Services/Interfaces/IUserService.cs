using App.GroupChat.Services.Entities;

namespace App.GroupChat.Services.Services.Interfaces {
    public interface IUserService {
        Task<bool> AddUserAsync(UserDto userDto);
        Task<bool> EditUserAsync(UserProfileDto userProfileDto, long userId);
        Task<ICollection<UserProfileDto>> GetUsersAsync();
        Task<UserProfileDto> GetUserByIdAsync(long userId);
    }
}
