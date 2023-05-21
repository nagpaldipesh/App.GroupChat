using App.GroupChat.Services.Entities;

namespace App.GroupChat.Services.Services.Interfaces {
    public interface IGroupMemberService {

        Task<bool> AddMembersIntoGroupAsync(ICollection<GroupMemberDto> memberDtos);
        Task<bool> RemoveMemberFromGroupAsync(long groupId, long userId);
    }
}
