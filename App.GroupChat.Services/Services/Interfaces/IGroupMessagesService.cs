using App.GroupChat.Services.Entities;

namespace App.GroupChat.Services.Services.Interfaces {
    public interface IGroupMessageService {
        Task<GroupMessageDto> PostMessageAsync(GroupMessageDto message);
    }
}
