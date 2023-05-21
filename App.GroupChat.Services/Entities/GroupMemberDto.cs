using System.ComponentModel.DataAnnotations;

namespace App.GroupChat.Services.Entities {
    public class GroupMemberDto {
        public long GroupMemberId { get; set; }
        [Required]
        public long GroupId { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}
