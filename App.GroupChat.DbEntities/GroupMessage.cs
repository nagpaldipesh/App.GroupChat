using System.ComponentModel.DataAnnotations;

namespace App.GroupChat.DbEntities {
    public class GroupMessage {
        public long GroupMessageId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public long GroupId { get; set; }
        [Required] 
        public long UserId{ get; set; }
        public DateTime MessagedOn { get; set; }
        [Required]
        public long TotalLikes { get; set; } = 0;
        public Group Group { get; set; }
        public User User { get; set; }
    }
}
