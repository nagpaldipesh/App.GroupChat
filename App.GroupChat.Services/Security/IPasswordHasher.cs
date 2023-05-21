
namespace App.GroupChat.Services.Security {
    public interface IPasswordHasher {
        string GetHashPassword(string password);
    }
}
