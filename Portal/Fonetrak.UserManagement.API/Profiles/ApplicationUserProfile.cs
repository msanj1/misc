using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.Models;
using Fonetrak.UserManagement.API.ValueResolvers;
using Microsoft.AspNetCore.Identity;

namespace Fonetrak.UserManagement.API.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Claims, opt => opt.MapFrom<ApplicationUserToUserDtoResolver>());
            CreateMap<UserForRegistrationDto, UserDto>();
            CreateMap<UserForRegistrationDto, ApplicationUser>();
            CreateMap<UserForUpdateDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserForUpdateDto>()
                .ForMember(dest => dest.Claims, opt => opt.MapFrom<ApplicationUserToUserForUpdateDto>()); ;
        }
    }
}
