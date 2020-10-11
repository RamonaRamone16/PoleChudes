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
                    cfg.AddProfile(new DomainToWordGetModelProfile());
                    cfg.AddProfile(new DomainToMatchGetModelProfile());
                    cfg.AddProfile(new MatchCreateModelToDomainProfile());
                    cfg.AddProfile(new DomainToWordModel());
                    cfg.AddProfile(new DomainToMatchModelProfile());
                    cfg.AddProfile(new UserCreateModelToDomainProfile());
                    cfg.AddProfile(new UserEditModelToDomainProfile());
                    cfg.AddProfile(new DomainToUserGetModelProfile());
                });
        }
    }
}
