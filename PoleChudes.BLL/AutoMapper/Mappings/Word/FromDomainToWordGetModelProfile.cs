using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models.Word;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class FromDomainToWordGetModelProfile : Profile
    {
        public FromDomainToWordGetModelProfile()
        {
            FromDomainToWordGetModelMappingProfile();
        }

        private void FromDomainToWordGetModelMappingProfile()
        {
            CreateMap<Word, WordGetModel>()
                .ForMember(target => target.AdminUserName, 
                source => source.MapFrom(x => x.Admin.UserName));
        }
    }
}
