using System.ComponentModel.DataAnnotations;

namespace App.GroupChat.DbEntities {
    public class Group {
        public long GroupId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long GroupCreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public ICollection<GroupMember> Members { get; set; }
    }
}
