using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CustomIdentityUser, UserInfoDto>();
            CreateMap<UserEditReadDto, CustomIdentityUser>();
        }
    }
}
