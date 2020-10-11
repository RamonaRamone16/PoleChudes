using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class DomainToMatchGetModelProfile : Profile
    {
        public DomainToMatchGetModelProfile()
        {
            FromMatchToMatchGetModelMappingProfile();
        }

        private void FromMatchToMatchGetModelMappingProfile()
        {
            CreateMap<Match, MatchGetModel>();
        }
    }
}
