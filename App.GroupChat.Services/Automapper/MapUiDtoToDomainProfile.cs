using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using AutoMapper;

namespace App.GroupChat.Services.Automapper {
    public class MapUiDtoToDomainProfile : Profile {
        public MapUiDtoToDomainProfile() {
            CreateMap<UserDto, User>();
            CreateMap<GroupDto, Group>()
                .ForMember(d => d.IsDeleted, src => src.Equals(false));
            CreateMap<GroupMemberDto, GroupMember>()
                .ForMember(d => d.IsDeleted, src => src.Equals(false))
                .ForMember(d => d.JoinedOn, src => src.Equals(DateTime.Now));
            CreateMap<GroupMessageDto, GroupMessage>()
                .ForMember(d => d.MessagedOn, src => src.Equals(DateTime.Now));
        }
    }
}
