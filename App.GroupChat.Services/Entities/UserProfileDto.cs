using System.ComponentModel.DataAnnotations;

namespace App.GroupChat.Services.Entities {
    public class UserProfileDto {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(25)]
        public string Username { get; set; }
    }
}
