using App.GroupChat.Api.Controllers;
using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Services;
using App.GroupChat.UnitTest.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Linq.Expressions;

namespace App.GroupChat.Api.UnitTests.Contolleres {
    [TestFixture]
    public class GroupMemberControllerTests {
        private IGenericRepository<Group> _groupRepository;
        private IGenericRepository<GroupMember> _groupMemberRepository;
        private GroupMemberController _controller;
        [SetUp] public void SetUp() {
            _groupRepository = Substitute.For<IGenericRepository<Group>>();
            _groupMemberRepository = Substitute.For<IGenericRepository<GroupMember>>();
            var groupService = new GroupMemberService(_groupRepository, _groupMemberRepository);
            _controller = new GroupMemberController(groupService);
        }

        [Test]
        public async Task AddMembersIntoGroup_ShouldAddMembersIntoGroup() {
            var members = GroupHelper.GetMembers().ToList();
            var group = GroupHelper.GetGroups().First();
            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(new List<Group> { group }.AsQueryable().BuildMock());

            var response = await _controller.AddMembersIntoGroupAsync(members);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBe(true);
        }

        [Test]
        public void RemoveMemberFromGroup_ShouldThrowsNotFoundException_WhenGroupIsNotFound() {

            _groupMemberRepository.FindBy(Arg.Any<Expression<Func<GroupMember, bool>>>()).Returns(new List<GroupMember>().AsQueryable().BuildMock());

            Assert.ThrowsAsync<NotFoundException>(() => _controller.RemoveMemberFromGroupAsync(groupId: 1, userId: 12345)).Message.ShouldBe("Invalid details");
        }
        [Test]
        public async Task RemoveMemberFromGroup_ShouldReturnTrue_AfterRemovingTheUser() {
            var group = GroupHelper.GetGroups().First();
            _groupMemberRepository.FindBy(Arg.Any<Expression<Func<GroupMember, bool>>>()).Returns(new List<GroupMember> { group.Members.First() }.AsQueryable().BuildMock());

            var response = await _controller.RemoveMemberFromGroupAsync(groupId: 1, userId: 12345);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBe(true);
        }
    }
}
