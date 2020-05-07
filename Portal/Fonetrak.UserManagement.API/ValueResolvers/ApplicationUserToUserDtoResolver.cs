using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Fonetrak.UserManagement.API.ValueResolvers
{
    public class ApplicationUserToUserDtoResolver : IValueResolver<ApplicationUser,UserDto,List<ClaimDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ApplicationUserToUserDtoResolver(UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public List<ClaimDto> Resolve(ApplicationUser source, UserDto destination, List<ClaimDto> destMember, ResolutionContext context)
        {
            var claims = _userManager.GetClaimsAsync(source).Result;
            return _mapper.Map<List<ClaimDto>>(claims);
        }
    }
}
