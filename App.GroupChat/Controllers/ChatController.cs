using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Hubs;
using App.GroupChat.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace App.GroupChat.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase {
        private IHubContext<ChatHub> _chat;
        private IGroupMessageService _groupMessagesService;
        public ChatController(IHubContext<ChatHub> chat, IGroupMessageService groupMessagesService) {
            _chat = chat;
            _groupMessagesService = groupMessagesService;
        }

        [HttpPost("JoinGroup/{connectionId}/{groupId}")]
        public async Task<IActionResult> JoinRoomAsync(string connectionId, long groupId) {
            await _chat.Groups.AddToGroupAsync(connectionId, groupId.ToString());
            return Ok();
        }
        [HttpPost("LeaveGroup/{connectionId}/{groupId}")]
        public async Task<IActionResult> LeaveRoomAsync(string connectionId, string groupId) {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, groupId.ToString());
            return Ok();
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessageAsync(
        [FromBody] GroupMessageDto messageDto) {
            var message = await _groupMessagesService.PostMessageAsync(messageDto);

            await _chat.Clients.Group(message.GroupId.ToString())
                .SendAsync("ReceiveMessage", new {
                    Text = message.Message,
                    UserId = message.UserId,
                    Name = message.User.FirstName + ' ' + message.User.LastName,
                    Timestamp = message.MessagedOn
                });
            return Ok();
        }
    }
}
