using App.GroupChat.Services.Automapper;
using AutoMapper;

namespace App.GroupChat.UnitTest.Common.Automapper {
    public static class AutomapperConfiguration {
        public static IMapper Configure() {
            var mapperConfig = new MapperConfiguration(x => {
                x.AddProfile(new MapUiDtoToDomainProfile());
                x.AddProfile(new MapDomainToUiDtoProfile());
            });

            return mapperConfig.CreateMapper();
        }
    }
}
