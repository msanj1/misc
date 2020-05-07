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
    public class ApplicationUserToUserForUpdateDto : IValueResolver<ApplicationUser, UserForUpdateDto, List<ClaimForUpdateDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ApplicationUserToUserForUpdateDto(UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public List<ClaimForUpdateDto> Resolve(ApplicationUser source, UserForUpdateDto destination, List<ClaimForUpdateDto> destMember, ResolutionContext context)
        {
            var claims = _userManager.GetClaimsAsync(source).Result;
            return _mapper.Map<List<ClaimForUpdateDto>>(claims);
        }
    }
}
