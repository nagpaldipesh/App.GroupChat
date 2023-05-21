
namespace App.GroupChat.Api.Auth.Interfaces {
    public interface ITokenService {
        string GenerateToken(string username, int roleId, long userId);
    }
}
