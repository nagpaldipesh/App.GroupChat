using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace App.GroupChat.Services.Security {
    public class PasswordHasher: IPasswordHasher {
        private string hashKey = "x/A?D(G+KbPdSgVkYp3s6v9y$B&E)H@M";
        public string GetHashPassword(string password) {
            byte[] salt = Encoding.Unicode.GetBytes(hashKey);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}
