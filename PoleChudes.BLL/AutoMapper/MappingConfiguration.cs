using AutoMapper;
using PoleChudes.BLL.AutoMapper.Mappings;

namespace PoleChudes.BLL.AutoMapper
{
    public class MappingConfiguration
    {
        public MapperConfiguration RefisterMappings()
        {
            return new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new WordCreateModelToDomainProfile());
                    cfg.AddProfile(new WordEditModelToDomainProfile());
                    cfg.AddProfile(new FromDomainToWordGetModelProfile());
                    cfg.AddProfile(new FromDomainToMatchGetModelProfile());
                    cfg.AddProfile(new MatchCreateModelToDomainProfile());
                    cfg.AddProfile(new FromDomainToWordModel());
                });
        }
    }
}
