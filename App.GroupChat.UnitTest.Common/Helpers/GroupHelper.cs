using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;

namespace App.GroupChat.UnitTest.Common.Helpers {
    public class GroupHelper {
        public static List<Group> GetGroups() {
            return new List<Group>() {
                new Group() {
                    CreatedAt = new DateTime(2022,1,2),
                    GroupCreatedBy = UserHelper.GetUsers().First().UserId,
                    IsDeleted = false,
                    Name = "Friends",
                    GroupId = 1,
                    Members = new List<GroupMember>() {
                        new GroupMember() {
                            GroupId = 1,
                            GroupMemberId = 1,
                            JoinedOn = new DateTime(2022,1,2),
                            IsDeleted = false,
                            UserId =UserHelper.GetUsers().First().UserId,
                        },
                        new GroupMember() {
                            GroupId = 1,
                            GroupMemberId = 22,
                            JoinedOn = new DateTime(2022,1,3),
                            IsDeleted = false,
                            UserId =UserHelper.GetUsers()[1].UserId,
                        }
                    }
                },
                new Group() {
                    CreatedAt = new DateTime(2022,3,2),
                    GroupCreatedBy = UserHelper.GetUsers()[1].UserId,
                    IsDeleted = false,
                    Name = "Friends and benefits",
                    GroupId = 2,
                    Members = new List<GroupMember>() {
                        new GroupMember() {
                            GroupId = 2,
                            GroupMemberId = 3,
                            JoinedOn = new DateTime(2022,1,2),
                            IsDeleted = false,
                            UserId =UserHelper.GetUsers()[1].UserId,
                        },
                        new GroupMember() {
                            GroupId = 2,
                            GroupMemberId = 4,
                            JoinedOn = new DateTime(2022,1,3),
                            IsDeleted = false,
                            UserId =UserHelper.GetUsers()[0].UserId,
                        },
                        new GroupMember() {
                            GroupId = 2,
                            GroupMemberId = 5,
                            JoinedOn = new DateTime(2022,1,3),
                            IsDeleted = false,
                            UserId =UserHelper.GetUsers()[2].UserId,
                        }
                    }
                },
                new Group() {
                    CreatedAt = new DateTime(2022,1,2),
                    GroupCreatedBy = UserHelper.GetUsers().First().UserId,
                    IsDeleted = false,
                    Name = "Group 1",
                    GroupId = 3,
                    Members = new List<GroupMember>() {
                        new GroupMember() {
                            GroupId = 3,
                            GroupMemberId = 6,
                            JoinedOn = new DateTime(2022,1,2),
                            IsDeleted = false,
                            UserId =UserHelper.GetUsers().First().UserId,
                        },
                        new GroupMember() {
                            GroupId = 3,
                            GroupMemberId = 7,
                            JoinedOn = new DateTime(2022,1,3),
                            IsDeleted = false,
                            UserId =UserHelper.GetUsers()[1].UserId,
                        }
                    }
                },
            };
        }

        public static List<GroupMemberDto> GetMembers() {
            return new List<GroupMemberDto>() {
                new GroupMemberDto() {
                    GroupId = 1,
                    GroupMemberId = 1,
                    UserId = UserHelper.GetUsers().First().UserId
                },
                new GroupMemberDto() {
                    GroupId = 1,
                    GroupMemberId = 2,
                    UserId = UserHelper.GetUsers()[1].UserId
                },
                new GroupMemberDto() {
                    GroupId = 1,
                    GroupMemberId = 3,
                    UserId = UserHelper.GetUsers()[2].UserId
                }
            };
        }
    }
}
