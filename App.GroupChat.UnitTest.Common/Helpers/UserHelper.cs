using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using System.Diagnostics.Contracts;

namespace App.GroupChat.UnitTest.Common.Helpers {
    public static class UserHelper {
        public static List<UserDto> GetUserDtos() {
            return new List<UserDto>() {
                new UserDto() {
                Email = "dipesh.nagpal96@gmail.com",
                FirstName = "Dipesh",
                LastName = "Super Admin",
                Password = "password",
                Username = "dipeshnagpal",
                UserRoleId = (int)App.GroupData.Shared.UserRoles.SuperAdmin
                },
                new UserDto() {
                Email = "dipeshadmin@gmail.com",
                FirstName = "Dipesh",
                LastName = "Admin",
                Password = "password",
                Username = "dipeshnagpal2",
                UserRoleId = (int)App.GroupData.Shared.UserRoles.Admin
                },
                new UserDto() {
                Email = "dipeshuser@gmail.com",
                FirstName = "Dipesh",
                LastName = "User",
                Password = "password",
                Username = "dipeshnagpal3",
                UserRoleId = (int)App.GroupData.Shared.UserRoles.User
                }
            };
        }
        public static List<User> GetUsers() {
            return new List<User>() {
                new User() {
                    Email = "dipesh.nagpal96@gmail.com",
                    FirstName = "Dipesh",
                    LastName = "Super Admin",
                    Username = "dipeshnagpal",
                    UserRoleId = (int)App.GroupData.Shared.UserRoles.SuperAdmin,
                    UserId = 1,
                    UserPassword = new UserPassword() {
                        Password = "password",
                        UserId = 1
                    }
                },
                new User() {
                    Email = "dipeshadmin@gmail.com",
                    FirstName = "Dipesh",
                    LastName = "Admin",
                    Username = "dipeshnagpal2",
                    UserId = 2,
                    UserRoleId = (int)App.GroupData.Shared.UserRoles.Admin,
                    UserPassword = new UserPassword() {
                        Password = "password",
                        UserId = 2
                    }
                },
                new User() {
                   Email = "dipeshuser@gmail.com",
                    FirstName = "Dipesh",
                    LastName = "User",
                    Username = "dipeshnagpal3",
                    UserId = 1,
                    UserRoleId = (int)App.GroupData.Shared.UserRoles.User,
                    UserPassword = new UserPassword() {
                        Password = "password",
                        UserId = 1
                    }
                }
            };
        }
    }
}
