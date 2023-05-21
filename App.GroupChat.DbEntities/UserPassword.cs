using System.ComponentModel.DataAnnotations.Schema;

namespace App.GroupChat.DbEntities {
    public class UserPassword {
        public long Id { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public string Password { get; set; }
        public User User { get; set; }
    }
}
