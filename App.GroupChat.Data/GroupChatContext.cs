using App.GroupChat.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace App.GroupChat.Data {
    public class GroupChatContext : DbContext {
        public GroupChatContext(DbContextOptions<GroupChatContext> options) : base(options) {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            RunFluentApiSchemaDefinitions(builder);
            AddSeedDataIntoTable(builder);

        }

        private static void RunFluentApiSchemaDefinitions(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().HasKey(s => s.UserId);
            modelBuilder.Entity<UserPassword>().HasKey(s => s.Id);
            modelBuilder.Entity<Role>().HasKey(s => s.RoleId);

            modelBuilder.Entity<UserPassword>()
                .HasOne(u => u.User)
                .WithOne(x => x.UserPassword)
                .HasForeignKey<UserPassword>(f => f.UserId);

            modelBuilder.Entity<GroupMember>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Members)
                .HasForeignKey(f => f.GroupId);
        }

        private static void AddSeedDataIntoTable(ModelBuilder modelBuilder) {
            
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = (int)App.GroupData.Shared.UserRoles.SuperAdmin, RoleName = "Super Admin" });
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = (int)App.GroupData.Shared.UserRoles.Admin, RoleName = "Admin" });
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = (int)App.GroupData.Shared.UserRoles.User, RoleName = "User" });

            modelBuilder.Entity<User>().HasData(new User {
                FirstName = "Dipesh",
                LastName = "Nagpal",
                Username = "dipeshnagpal",
                Email = "dipesh.nagpal96@gmail.com",
                UserId = 1,
                UserRoleId = (int)App.GroupData.Shared.UserRoles.SuperAdmin,
            },
            new User {
                FirstName = "Dipesh",
                LastName = "Nagpal",
                Username = "dipeshnagpal96",
                Email = "dipesh.nagpal@gmail.com",
                UserId = 2,
                UserRoleId = (int)App.GroupData.Shared.UserRoles.Admin,
            },
            new User {
                FirstName = "Dipesh",
                LastName = "User",
                Username = "dipeshuser",
                Email = "dipesh.nagpal1996@gmail.com",
                UserId = 2,
                UserRoleId = (int)App.GroupData.Shared.UserRoles.User,
            });

            modelBuilder.Entity<UserPassword>().HasData(new UserPassword {
                Id = 1,
                UserId = 1,
                Password = "AZn/6TX1xRg7bb1oGZP2kt2WhtB1jkwneL289mWmy8Q=" //Password404!
            },
            new UserPassword {
                Id = 2,
                UserId = 2,
                Password = "AZn/6TX1xRg7bb1oGZP2kt2WhtB1jkwneL289mWmy8Q=" //Password404!
            },
            new UserPassword {
                Id = 3,
                UserId = 3,
                Password = "AZn/6TX1xRg7bb1oGZP2kt2WhtB1jkwneL289mWmy8Q=" //Password404!
            });
        }
    }
}