using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.GroupChat.DbEntities {
    public class GroupMember {
        public long GroupMemberId { get; set; }
        [Required]
        [ForeignKey("Group")]
        public long GroupId { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public DateTime JoinedOn { get; set; }
        [Required]
        public bool IsDeleted { get; set;}
        public Group Group { get; set; }
        public User User { get; set; }
    }
}
