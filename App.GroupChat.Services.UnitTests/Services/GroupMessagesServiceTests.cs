using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.Services.Services.Interfaces;
using App.GroupChat.Services.Services;
using App.GroupChat.UnitTest.Common.Automapper;
using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using App.GroupChat.DbEntities;
using App.GroupChat.UnitTest.Common.Helpers;
using Shouldly;
using App.GroupChat.Services.Entities;

namespace App.GroupChat.Services.UnitTests.Services {
    [TestFixture]
    public class GroupMessagesServiceTests {
        private IMapper _mapper;
        private IGenericRepository<GroupMessage> _groupMessageRepository;
        private IGroupMessageService _groupService;
        [SetUp]
        public void Setup() {
            _mapper = AutomapperConfiguration.Configure();
            _groupMessageRepository = Substitute.For<IGenericRepository<GroupMessage>>();
            _groupService = new GroupMessagesService(_mapper, _groupMessageRepository);
        }
        [Test]
        public async Task PostMessage_ShouldSaveMessageIntoDB() {
            var message = GroupMessageHelper.GetGroupMessageDtos()[0];
            var response = await _groupService.PostMessageAsync(message);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<GroupMessageDto>();
            response.Message.ShouldBe(message.Message);
        }
    }
}
