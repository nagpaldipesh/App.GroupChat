using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using AutoMapper;

namespace App.GroupChat.Services.Automapper {
    public class MapDomainToUiDtoProfile : Profile {
        public MapDomainToUiDtoProfile() {
            CreateMap<User, UserDto>();
            CreateMap<User, UserProfileDto>(); 
            CreateMap<Group , GroupDto>();
            CreateMap<GroupMember, GroupMemberDto>();
            CreateMap<GroupMessage, GroupMessageDto>();
                //.ForMember(d => d.Password, src => src.MapFrom(m => m.UserPassword.Password));
        }
    }
}
