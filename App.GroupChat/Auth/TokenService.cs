using App.GroupChat.Api.Auth.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.GroupChat.Api.Auth {
    public class TokenService : ITokenService {
        private readonly string secret;
        public TokenService(IConfiguration configuration) {
            secret = configuration["JWT:Key"];
        }
        public string GenerateToken(string username, int roleId, long userId) {
            var symmetricKey = Encoding.UTF8.GetBytes(secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Role, roleId.ToString()),
                            new Claim("UserId", userId.ToString())
                        }),

                Expires = now.AddMinutes(Convert.ToInt32(10)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
