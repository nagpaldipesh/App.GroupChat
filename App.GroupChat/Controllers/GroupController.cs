using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.GroupChat.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase {
        private IGroupService _groupService;
        public GroupController(IGroupService groupService) {
            _groupService = groupService;
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetGroupsByNameAsync([FromRoute] string name, int pageNumber, int pageSize) {
            var groups = await _groupService.GetGroupsByNameAsync(name, pageNumber, pageSize);
            return Ok(groups);
        }
        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetUserGroupsAsync(int pageNumber, int pageSize) {
            var userId = Convert.ToInt64(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
            var groups = await _groupService.GetUserGroupsAsync(userId, pageNumber, pageSize);
            return Ok(groups);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupDetailsByGroupIdAsync(long groupId) {
            var group = await _groupService.GetGroupDetailsByGroupIdAsync(groupId);
            return Ok(group);
        }
        [HttpPost]
        public async Task<IActionResult> AddGroupAsync([FromBody] GroupDto groupDto) {
            var userId = Convert.ToInt64(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
            var group = await _groupService.AddGroupAsync(groupDto, userId);
            return Ok(group);
        }
    }
}
