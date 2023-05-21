using App.GroupChat.Api.Auth.Interfaces;
using App.GroupChat.Api.Auth;
using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.Data.Repositories;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Services.Interfaces;
using App.GroupChat.Services.Services;
using App.GroupChat.Services.Security;

namespace App.GroupChat.Api.Extensions {
    public static class ServicesExtension {
        public static void AddServicesIntoServiceCollection(this IServiceCollection services) {
            
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
            services.AddScoped<IGenericRepository<Group>, GenericRepository<Group>>();
            services.AddScoped<IGenericRepository<GroupMember>, GenericRepository<GroupMember>>();
            services.AddScoped<IGenericRepository<GroupMessage>, GenericRepository<GroupMessage>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupMessageService, GroupMessagesService>();
            services.AddScoped<IGroupMemberService, GroupMemberService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
        }
    }
}
