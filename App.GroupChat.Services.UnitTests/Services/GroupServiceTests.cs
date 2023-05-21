using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Exceptions;
using App.GroupChat.Services.Services;
using App.GroupChat.Services.Services.Interfaces;
using App.GroupChat.UnitTest.Common.Automapper;
using App.GroupChat.UnitTest.Common.Helpers;
using AutoMapper;
using MockQueryable.Moq;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Linq.Expressions;

namespace App.GroupChat.Services.UnitTests.Services {
    [TestFixture]
    public class GroupServiceTests {
        private IMapper _mapper;
        private IGenericRepository<Group> _groupRepository;
        private IGroupService _groupService;
        [SetUp]
        public void Setup() {
            _mapper = AutomapperConfiguration.Configure();
            _groupRepository = Substitute.For<IGenericRepository<Group>>();
            _groupService = new GroupService(_groupRepository, _mapper);
        }

        [Test]
        public async Task GetGroupsByName_ShouldReturnGroups() {

            var groups = GroupHelper.GetGroups();
            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(groups.AsQueryable().BuildMock());

            var response = await _groupService.GetGroupsByNameAsync("fr", 1, 1);
            Assert.IsNotNull(response);
            response.ShouldBeOfType<GroupSummary>();
            response.ShouldNotBeNull();
            response.Groups.ShouldNotBeNull();
            response.Groups.Count.ShouldBe(1);
            response.TotalCount.ShouldBe(3);
        }
        [Test]
        public async Task GetGroupsByName_ShouldReturnEmptyGroups() {

            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(new List<Group>().AsQueryable().BuildMock());

            var response = await _groupService.GetGroupsByNameAsync("adsasdasd", 1, 1);
            Assert.IsNotNull(response);
            response.ShouldBeOfType<GroupSummary>();
            response.ShouldNotBeNull();
            response.Groups.ShouldNotBeNull();
            response.Groups.Count.ShouldBe(0);
            response.TotalCount.ShouldBe(0);
        }
        [Test]
        public async Task GetUserGroups_ShouldReturnGroups() {
            
            var groups = GroupHelper.GetGroups();
            var userId = groups.First().GroupCreatedBy;

            var userGroups = groups.Where(cond => cond.GroupCreatedBy == userId).ToList();

            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(userGroups.AsQueryable().BuildMock());

            var response = await _groupService.GetUserGroupsAsync(userId, 1, 1);
            Assert.IsNotNull(response);
            response.ShouldBeOfType<GroupSummary>();
            response.ShouldNotBeNull();
            response.Groups.ShouldNotBeNull();
            response.Groups.Count.ShouldBe(1);
            response.TotalCount.ShouldBe(2);
        }
        [Test]
        public async Task GetUserGroups_ShouldReturnGroupsForPage2() {
            
            var groups = GroupHelper.GetGroups();
            var userId = groups.First().GroupCreatedBy;

            var userGroups = groups.Where(cond => cond.GroupCreatedBy == userId).ToList();

            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(userGroups.AsQueryable().BuildMock());

            var response = await _groupService.GetUserGroupsAsync(userId, 2, 1);
            Assert.IsNotNull(response);
            response.ShouldBeOfType<GroupSummary>();
            response.ShouldNotBeNull();
            response.Groups.ShouldNotBeNull();
            response.Groups.Count.ShouldBe(1);
            response.TotalCount.ShouldBe(2);
        }
        [Test]
        public async Task GetUserGroups_ShouldReturnEmptyGroupsForPage3() {
            
            var groups = GroupHelper.GetGroups();
            var userId = groups.First().GroupCreatedBy;

            var userGroups = groups.Where(cond => cond.GroupCreatedBy == userId).ToList();

            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(userGroups.AsQueryable().BuildMock());

            var response = await _groupService.GetUserGroupsAsync(userId, 3, 1);
            Assert.IsNotNull(response);
            response.ShouldBeOfType<GroupSummary>();
            response.ShouldNotBeNull();
            response.Groups.ShouldNotBeNull();
            response.Groups.Count.ShouldBe(0);
            response.TotalCount.ShouldBe(2);
        }
        [Test]
        public async Task GetGroupDetailsByGroupId_ShouldReturnGroupWhenGroupExists() {
            
            var group = GroupHelper.GetGroups().First();
            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(new List<Group> { group }.AsQueryable().BuildMock());

            var response = await _groupService.GetGroupDetailsByGroupIdAsync(group.GroupId);
            Assert.IsNotNull(response);
            response.ShouldBeOfType<GroupDto>();
            response.ShouldNotBeNull();
            response.GroupId.ShouldBe(group.GroupId);
        }
        [Test]
        public void GetGroupDetailsByGroupId_ShouldThrowNotFoundException_WhenGroupNotExists() {
            
            var group = GroupHelper.GetGroups().First();
            _groupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(new List<Group>().AsQueryable().BuildMock());
            Assert.ThrowsAsync<NotFoundException>(() => _groupService.GetGroupDetailsByGroupIdAsync(group.GroupId));
        }
        [Test]
        public async Task AddGroup_ShouldAddGroupIntoDatabase() {
            var group = GroupHelper.GetGroups().First();
            var groupDto = _mapper.Map<GroupDto>(group);

            var response = await _groupService.AddGroupAsync(groupDto, group.GroupCreatedBy);
            response.ShouldNotBeNull();
        }
    }
}
