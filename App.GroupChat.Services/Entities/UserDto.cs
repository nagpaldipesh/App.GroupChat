
using System.ComponentModel.DataAnnotations;

namespace App.GroupChat.Services.Entities {
    public class UserDto : UserProfileDto {
        public int UserId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int UserRoleId { get; set; }
    }
}
