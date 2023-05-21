using App.GroupChat.Services.Entities;

namespace App.GroupChat.Services.Services.Interfaces {
    public interface ILoginService {
        Task<UserDto> ValidateUserCreds(UserLoginDto userLoginDto);
    }
}
