using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class WordEditModelToDomainProfile : Profile
    {
        public WordEditModelToDomainProfile()
        {
            WordEditModelToWordMappingConfig();
        }

        private void WordEditModelToWordMappingConfig()
        {
            CreateMap<WordEditModel, Word>().ReverseMap();
        }
    }
}
