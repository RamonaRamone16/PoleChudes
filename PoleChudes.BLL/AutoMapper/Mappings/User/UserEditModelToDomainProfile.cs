using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models.User;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class UserEditModelToDomainProfile : Profile
    {
        public UserEditModelToDomainProfile()
        {
            UserEditModelToUserMappingProfile();
        }

        private void UserEditModelToUserMappingProfile()
        {
            CreateMap<UserEditModel, User>().ReverseMap();
        }
    }
}
