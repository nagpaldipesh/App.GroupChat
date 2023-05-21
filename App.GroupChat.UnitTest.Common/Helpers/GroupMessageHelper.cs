using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;

namespace App.GroupChat.UnitTest.Common.Helpers {
    public class GroupMessageHelper {
        public static List<GroupMessage> GetGroupMessages() {
            return new List<GroupMessage>() {
                new GroupMessage() {
                    GroupId = 1,
                    GroupMessageId = 1,
                    Message = "This is the messagae",
                    UserId = UserHelper.GetUsers().First().UserId,
                    MessagedOn = DateTime.Now,
                },
                new GroupMessage() {
                    GroupId = 1,
                    GroupMessageId = 2,
                    Message = "This is the message 2",
                    UserId = UserHelper.GetUsers()[1].UserId,
                    MessagedOn = DateTime.Now,
                }
            };
        }
        public static List<GroupMessageDto> GetGroupMessageDtos() {
            return new List<GroupMessageDto>() {
                new GroupMessageDto() {
                    Message = "This is the messagae",
                    UserId = UserHelper.GetUsers().First().UserId,
                    GroupId = 1,
                    MessagedOn= DateTime.Now,
                    User = UserHelper.GetUserDtos().First()
                },
                new GroupMessageDto() {
                    Message = "This is the message 2",
                    UserId = UserHelper.GetUsers().First().UserId,
                    GroupId = 1,
                    MessagedOn= DateTime.Now,
                    User = UserHelper.GetUserDtos()[1]
                }
            };
        }
    }
}
