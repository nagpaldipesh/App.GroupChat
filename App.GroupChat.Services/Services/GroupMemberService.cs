using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.GroupChat.Services.Services {
    public class GroupMemberService : IGroupMemberService {
        private readonly IGenericRepository<Group> _groupRepository;
        private readonly IGenericRepository<GroupMember> _groupMemberRepository;
        public GroupMemberService(IGenericRepository<Group> groupRepository, IGenericRepository<GroupMember> groupMemberRepository) {
            _groupRepository = groupRepository;
            _groupMemberRepository = groupMemberRepository;
        }
        public async Task<bool> AddMembersIntoGroupAsync(ICollection<GroupMemberDto> memberDtos) {
            var groupId = memberDtos.First().GroupId;
            var group = await _groupRepository.FindBy(cond => cond.GroupId == groupId).Include(inc => inc.Members).FirstOrDefaultAsync();
            if (group != null) {
                foreach (var memberDto in memberDtos) {
                    var member = group.Members.Where(cond => cond.UserId == memberDto.UserId && !cond.IsDeleted).FirstOrDefault();
                    if (member == null) {
                        group.Members.Add(new GroupMember() {
                            GroupId = groupId,
                            UserId = memberDto.UserId,
                            IsDeleted = false,
                            JoinedOn = DateTime.UtcNow
                        });
                    }
                    await _groupRepository.SaveAsync();
                };
            }
            return true;
        }
        public async Task<bool> RemoveMemberFromGroupAsync(long groupId, long userId) {
            var groupMember = await _groupMemberRepository.FindBy(cond => cond.GroupId == groupId && cond.UserId == userId).FirstOrDefaultAsync();
            if (groupMember == null) {
                throw new NotFoundException("Invalid details");
            }

            groupMember.IsDeleted = true;

            _groupMemberRepository.Update(groupMember);
            await _groupMemberRepository.SaveAsync();
            return true;
        }
    }
}
