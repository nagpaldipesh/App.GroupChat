
using App.GroupChat.Services.Entities;

namespace App.GroupChat.Services.Services.Interfaces {
    public interface IGroupService {
        Task<GroupSummary> GetUserGroupsAsync(long UserId, int pageNumber, int pageSize);
        Task<GroupSummary> GetGroupsByNameAsync(string Name, int pageNumber, int pageSize);
        Task<GroupDto> AddGroupAsync(GroupDto group, long userId);
        Task<GroupDto> GetGroupDetailsByGroupIdAsync(long groupId);
    }
}
