using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Services.Interfaces;
using App.GroupChat.Services.Services;
using NSubstitute;
using NUnit.Framework;
using App.GroupChat.UnitTest.Common.Helpers;
using Shouldly;
using System.Linq.Expressions;
using MockQueryable.Moq;
using App.GroupChat.Services.Exceptions;

namespace App.GroupChat.Services.UnitTests.Services {
    [TestFixture]
    public class GroupMemberServiceTests {
        private IGenericRepository<GroupMember> _groupMemberRepository;
        private IGenericRepository<Group> _groupRepository;
        private IGroupMemberService _groupService;
        [SetUp]
        public void Setup() {
            _groupMemberRepository = Substitute.For<IGenericRepository<GroupMember>>();
            _groupRepository = Substitute.For<IGenericRepository<Group>>();
            _groupService = new GroupMemberService(_groupRepository, _groupMemberRepository);
        }
        [Test]
        public async Task AddMembersIntoGroup_ShouldAddMembersIntoGroup() {
            var members = GroupHelper.GetMembers().ToList();
            var group = GroupHelper.GetGroups().First();
            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(new List<Group> { group }.AsQueryable().BuildMock());

            var response = await _groupService.AddMembersIntoGroupAsync(members);
            response.ShouldBeTrue();
        }
        [Test]
        public void RemoveMemberFromGroup_ShouldThrowsNotFoundException_WhenGroupIsNotFound() {

            _groupMemberRepository.FindBy(Arg.Any<Expression<Func<GroupMember, bool>>>()).Returns(new List<GroupMember>().AsQueryable().BuildMock());

            Assert.ThrowsAsync<NotFoundException>(() => _groupService.RemoveMemberFromGroupAsync(groupId: 1, userId: 12345)).Message.ShouldBe("Invalid details");
        }
        [Test]
        public async Task RemoveMemberFromGroup_ShouldReturnTrue_AfterRemovingTheUser() {
            var group = GroupHelper.GetGroups().First();
            _groupMemberRepository.FindBy(Arg.Any<Expression<Func<GroupMember, bool>>>()).Returns(new List<GroupMember> { group.Members.First() } .AsQueryable().BuildMock());
            
            var response = await _groupService.RemoveMemberFromGroupAsync(groupId: 1, userId: 12345);
            response.ShouldBe(true);
        }
    }
}
