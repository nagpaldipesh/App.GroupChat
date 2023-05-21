using App.GroupChat.Data.Repositories.Interfaces;
using App.GroupChat.DbEntities;
using App.GroupChat.Services.Entities;
using App.GroupChat.Services.Services.Interfaces;
using AutoMapper;

namespace App.GroupChat.Services.Services {
    public class GroupMessagesService : IGroupMessageService {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<GroupMessage> _groupMessageRepository;

        public GroupMessagesService(IMapper mapper, IGenericRepository<GroupMessage> groupMessageRepository) {
            _mapper = mapper;
            _groupMessageRepository = groupMessageRepository;
        }

        public async Task<GroupMessageDto> PostMessageAsync(GroupMessageDto messageDto) {
            var message = _mapper.Map<GroupMessage>(messageDto);

            _groupMessageRepository.Add(message);
            await _groupMessageRepository.SaveAsync();
         
            return _mapper.Map<GroupMessageDto>(message);
        }


    }
}
