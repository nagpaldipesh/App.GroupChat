
namespace App.GroupChat.Services.Entities {
    public class GroupSummary {
        public ICollection<GroupDto> Groups { get; set; }
        public long TotalCount { get; set; }
        public long PageNumber { get; set; }
        public long PageSize { get; set; }
    }
}
