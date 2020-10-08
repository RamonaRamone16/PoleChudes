﻿using AutoMapper;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;

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
            CreateMap<Word, WordGetModel>();
        }
    }
}
