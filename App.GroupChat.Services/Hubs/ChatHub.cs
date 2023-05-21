using Microsoft.AspNetCore.SignalR;

namespace App.GroupChat.Services.Hubs {
    public class ChatHub : Hub {
        public string GetConnectionId() =>
        Context.ConnectionId;
    }
}
