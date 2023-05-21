using System.ComponentModel.DataAnnotations;

namespace App.GroupChat.Services.Entities {
    public class GroupDto {
        public long GroupId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long GroupCreatedBy { get; set; }
        public ICollection<GroupMemberDto> Members { get; set; }
    }
}
