using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.Models;

namespace Fonetrak.UserManagement.API.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserForRegistrationDto, UserDto>();
            CreateMap<UserForRegistrationDto, ApplicationUser>();
        }
    }
}
