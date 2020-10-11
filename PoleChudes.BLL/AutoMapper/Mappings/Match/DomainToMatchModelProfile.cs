using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class DomainToMatchModelProfile : Profile
    {
        public DomainToMatchModelProfile()
        {
            FromMatchToMatchModelMappingProfile();
        }

        private void FromMatchToMatchModelMappingProfile()
        {
            CreateMap<Match, MatchModel>()
                .ForMember(target => target.UserName,
                source => source.MapFrom(x => x.User.UserName));
        }
    }
}
