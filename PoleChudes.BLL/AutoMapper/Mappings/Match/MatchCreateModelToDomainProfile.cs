using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class MatchCreateModelToDomainProfile : Profile
    {
        public MatchCreateModelToDomainProfile()
        {
            MatchCreateModelToMatchMappingConfig();
        }

        private void MatchCreateModelToMatchMappingConfig()
        {
            CreateMap<MatchCreateModel, Match>();
        }
    }
}
