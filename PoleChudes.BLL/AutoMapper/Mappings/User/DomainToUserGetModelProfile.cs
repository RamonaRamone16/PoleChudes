using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models.User;
using System.Linq;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class DomainToUserGetModelProfile : Profile
    {
        public DomainToUserGetModelProfile()
        {
            UserToUserGetModelMappingProfile();
        }

        private void UserToUserGetModelMappingProfile()
        {
            CreateMap<User, UserGetModel>()
                .ForMember(target => target.Points,
                source => source.MapFrom(x => x.Matches.Select(x => x.Points).Sum()));
        }
    }
}
