using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.UserManagement.API.Models;

namespace Fonetrak.UserManagement.API.Profiles
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<ClaimForRegistrationDto, Claim>();
            CreateMap<Claim, ClaimDto>();
        }
    }
}
