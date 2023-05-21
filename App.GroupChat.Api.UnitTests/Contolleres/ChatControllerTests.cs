using App.GroupChat.Api.Controllers;
using App.GroupChat.Services.Hubs;
using App.GroupChat.Services.Services.Interfaces;
using App.GroupChat.UnitTest.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace App.GroupChat.Api.UnitTests.Contolleres {
    [TestFixture]
    public class ChatControllerTests {
        private IGroupMessageService _groupMessageService;
        private ChatController _controller;
        private IHubContext<ChatHub> _chat;
        [SetUp]
        public void Setup() {
            _chat = Substitute.For<IHubContext<ChatHub>>();
            _groupMessageService = Substitute.For<IGroupMessageService>();
            _controller = new ChatController(_chat, _groupMessageService);
        }

        [Test]
        public async Task JoinRoom_ShouldReturnOkStatusCode() {
            var response = await _controller.JoinRoomAsync("connectionId", 1);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkResult>();
        }
        [Test]
        public async Task LeaveRoom_ShouldReturnOkStatusCode() {
            var response = await _controller.LeaveRoomAsync("connectionId", "1");
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkResult>();
        }
        [Test]
        public async Task SendMessage_ShouldReturnOkStatusCode() {
            var message = GroupMessageHelper.GetGroupMessageDtos()[0];
            _groupMessageService.PostMessageAsync(message).Returns(message);
            var response = await _controller.SendMessageAsync(message);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkResult>();
        }
    }
}
