using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.GroupChat.Services.Services {
    public class GroupService : IGroupService {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Group> _groupRepository;
        public GroupService(IGenericRepository<Group> groupRepository, IMapper mapper) {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<GroupSummary> GetGroupsByNameAsync(string name, int pageNumber, int pageSize) {
            var query = _groupRepository.FindBy(cond => !cond.IsDeleted && cond.Name.Contains(name));
            var groups = await query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            return new GroupSummary() {
                Groups = _mapper.Map<ICollection<GroupDto>>(groups),
                TotalCount = await query.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<GroupSummary> GetUserGroupsAsync(long userId, int pageNumber, int pageSize) {
            var query = _groupRepository.FindBy(cond => !cond.IsDeleted && cond.Members.Any(cond2 => cond2.UserId == userId));
            var groups = await query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
            

            return new GroupSummary() {
                Groups = _mapper.Map<ICollection<GroupDto>>(groups),
                TotalCount = await query.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<GroupDto> AddGroupAsync(GroupDto groupDto, long userId) {
            var group = _mapper.Map<Group>(groupDto);
            if (group != null) {
                group.GroupCreatedBy = userId;
                group.CreatedAt = DateTime.UtcNow;
                group.IsDeleted = false;
                group.Members.Add(new GroupMember() {
                    UserId = userId,
                    JoinedOn = DateTime.UtcNow,
                    IsDeleted = false,
                });
                _groupRepository.Add(group);
                await _groupRepository.SaveAsync();
            }
            return _mapper.Map<GroupDto>(group);
        }

        public async Task<GroupDto> GetGroupDetailsByGroupIdAsync(long groupId) {
            var group = await _groupRepository.FindBy(cond => cond.GroupId == groupId).Include(inc => inc.Members).FirstOrDefaultAsync();
            if (group == null) {
                throw new NotFoundException("Group not found");
            }
            return _mapper.Map<GroupDto>(group);
        }
    }
}
