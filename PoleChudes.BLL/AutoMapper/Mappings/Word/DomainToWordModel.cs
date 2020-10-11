using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class DomainToWordModel : Profile
    {
        public DomainToWordModel()
        {
            FromWordToWordModelMappingProfile();
        }

        private void FromWordToWordModelMappingProfile()
        {
            CreateMap<Word, WordModel>()
                .ForMember(target => target.AdminUserName,
                source => source.MapFrom(x => x.Admin.UserName));
        }
    }
}
