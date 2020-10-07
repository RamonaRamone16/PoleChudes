using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class WordCreateModelToDomainProfile : Profile
    {
        public WordCreateModelToDomainProfile()
        {
            WordCreateModelToWordMappingConfig();
        }

        private void WordCreateModelToWordMappingConfig()
        {
            CreateMap<WordCreateModel, Word>();
        }
    }
}
