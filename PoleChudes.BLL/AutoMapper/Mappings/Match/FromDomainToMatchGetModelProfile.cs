using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class FromDomainToMatchGetModelProfile : Profile
    {
        public FromDomainToMatchGetModelProfile()
        {
            FromMatchToMatchGetModelMappingProfile();
        }

        private void FromMatchToMatchGetModelMappingProfile()
        {
            CreateMap<Match, MatchGetModel>();
        }
    }
}
