using System.ComponentModel.DataAnnotations;

namespace App.GroupChat.Services.Entities {
    public class GroupMessageDto {
        public long GroupMessageId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public long GroupId { get; set; }
        [Required]
        public long UserId { get; set; }
        public DateTime MessagedOn { get; set; }

        public UserDto User { get; set; }
    }
}
