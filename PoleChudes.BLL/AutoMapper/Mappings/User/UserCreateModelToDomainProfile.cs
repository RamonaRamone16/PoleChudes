using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models.User;

namespace PoleChudes.BLL.AutoMapper.Mappings
{
    public class UserCreateModelToDomainProfile : Profile
    {
        public UserCreateModelToDomainProfile()
        {
            UserCraeteModelToUserMappingProfile();
        }

        private void UserCraeteModelToUserMappingProfile()
        {
            CreateMap<UserCreateModel, User>();
        }
    }
}
