using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.GroupChat.Api.Controllers {
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupMemberController : ControllerBase {
        private IGroupMemberService _groupMemberService;
        public GroupMemberController(IGroupMemberService groupMemberService) {
            _groupMemberService = groupMemberService;
        }
        [HttpPost("GroupMembers")]
        public async Task<IActionResult> AddMembersIntoGroupAsync([FromBody] ICollection<GroupMemberDto> memberDtos) { 
            var group = await _groupMemberService.AddMembersIntoGroupAsync(memberDtos);
            return Ok(group);
        }
        [HttpDelete("Groups/{groupId}/Users/{userId}")]
        public async Task<IActionResult> RemoveMemberFromGroupAsync(long groupId, long userId) {
            await _groupMemberService.RemoveMemberFromGroupAsync(groupId, userId);
            return Ok();
        }
    }
}
