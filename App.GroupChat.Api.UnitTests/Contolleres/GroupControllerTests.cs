using App.GroupChat.Api.Controllers;
using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Services;
using App.GroupChat.UnitTest.Common.Automapper;
using App.GroupChat.UnitTest.Common.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Linq.Expressions;
using System.Security.Claims;

namespace App.GroupChat.Api.UnitTests.Contolleres {
    [TestFixture]
    public class GroupControllerTests {
        private IGenericRepository<Group> _groupRepository;
        private GroupController _controller;
        private IMapper _mapper;
        [SetUp]
        public void Setup() {
            _mapper = AutomapperConfiguration.Configure();
            _groupRepository = Substitute.For<IGenericRepository<Group>>();
            var groupService = new GroupService(_groupRepository, _mapper);
            _controller = new GroupController(groupService);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, "dipeshnagpal"),
                            new Claim(ClaimTypes.Role, "1"),
                            new Claim("UserId", "12345"),
                        }, "mock"));

            _controller = new GroupController(groupService);
            _controller.ControllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Test]
        public async Task GetGroupsByName_ShouldReturnGroups() {

            var groups = GroupHelper.GetGroups();
            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(groups.AsQueryable().BuildMock());

            var response = await _controller.GetGroupsByNameAsync("fr", 1, 1);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();

            var value = (GroupSummary)result.Value;
            value.ShouldNotBeNull();
            value.Groups.ShouldNotBeNull();
            value.Groups.Count.ShouldBe(1);
            value.TotalCount.ShouldBe(3);
        }
        [Test]
        public async Task GetGroupsByName_ShouldReturnEmptyGroups() {

            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(new List<Group>().AsQueryable().BuildMock());

            var response = await _controller.GetGroupsByNameAsync("adsasdasd", 1, 1);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();

            var value = (GroupSummary)result.Value;
            value.ShouldNotBeNull();
            value.Groups.ShouldNotBeNull();
            value.Groups.Count.ShouldBe(0);
            value.TotalCount.ShouldBe(0);
        }

        [Test]
        public async Task GetUserGroups_ShouldReturnGroups() {

            var groups = GroupHelper.GetGroups();
            var userId = groups.First().GroupCreatedBy;

            var userGroups = groups.Where(cond => cond.GroupCreatedBy == userId).ToList();

            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(userGroups.AsQueryable().BuildMock());

            var response = await _controller.GetUserGroupsAsync(1, 1);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();

            var value = (GroupSummary)result.Value;
            value.ShouldNotBeNull();
            value.Groups.ShouldNotBeNull();
            value.Groups.Count.ShouldBe(1);
            value.TotalCount.ShouldBe(2);
        }
        [Test]
        public async Task GetUserGroups_ShouldReturnGroupsForPage2() {

            var groups = GroupHelper.GetGroups();
            var userId = groups.First().GroupCreatedBy;

            var userGroups = groups.Where(cond => cond.GroupCreatedBy == userId).ToList();

            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(userGroups.AsQueryable().BuildMock());

            var response = await _controller.GetUserGroupsAsync(1, 1);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();

            var value = (GroupSummary)result.Value;
            value.ShouldNotBeNull();
            value.Groups.ShouldNotBeNull();
            value.Groups.Count.ShouldBe(1);
            value.TotalCount.ShouldBe(2);
        }
        [Test]
        public async Task GetUserGroups_ShouldReturnEmptyGroupsForPage3() {

            var groups = GroupHelper.GetGroups();
            var userId = groups.First().GroupCreatedBy;

            var userGroups = groups.Where(cond => cond.GroupCreatedBy == userId).ToList();

            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(userGroups.AsQueryable().BuildMock());

            var response = await _controller.GetUserGroupsAsync(3, 1);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();

            var value = (GroupSummary)result.Value;
            value.ShouldNotBeNull();
            value.Groups.ShouldNotBeNull();
            value.Groups.Count.ShouldBe(0);
            value.TotalCount.ShouldBe(2);
        }

        [Test]
        public async Task GetGroupDetailsByGroupId_ShouldReturnGroupWhenGroupExists() {

            var group = GroupHelper.GetGroups().First();
            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(new List<Group> { group }.AsQueryable().BuildMock());

            var response = await _controller.GetGroupDetailsByGroupIdAsync(group.GroupId);
            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();

            var value = (GroupDto)result.Value;
            value.ShouldNotBeNull();
            value.GroupId.ShouldBe(group.GroupId);
        }
        [Test]
        public void GetGroupDetailsByGroupId_ShouldThrowNotFoundException_WhenGroupNotExists() {

            var group = GroupHelper.GetGroups().First();
            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(new List<Group>().AsQueryable().BuildMock());
            Assert.ThrowsAsync<NotFoundException>(() => _controller.GetGroupDetailsByGroupIdAsync(group.GroupId));
        }
        [Test]
        public async Task AddGroup_ShouldAddGroupIntoDatabase() {
            var group = GroupHelper.GetGroups().First();
            var groupDto = _mapper.Map<GroupDto>(group);

            var response = await _controller.AddGroupAsync(groupDto);
            response.ShouldNotBeNull();
        }

    }
}
