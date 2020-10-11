using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class DomainToWordGetModelProfile : Profile
    {
        public DomainToWordGetModelProfile()
        {
            FromDomainToWordGetModelMappingProfile();
        }

        private void FromDomainToWordGetModelMappingProfile()
        {
            CreateMap<Word, WordGetModel>();
        }
    }
}
