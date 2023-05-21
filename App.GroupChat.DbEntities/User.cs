using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.GroupChat.DbEntities {
    public class User {
        public long UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(25)]
        public string Username { get; set; }
        [ForeignKey("UserRole")]
        [Required]
        public int UserRoleId { get; set; }
        public UserPassword UserPassword { get; set; }
        public Role Role { get; set; }
    }
}
