using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.UserManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fonetrak.UserManagement.API.Models
{
    [ApiController]
    [Route("api/users/{userId}/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ClaimsController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetClaims(string userId)
        {
            var userEntity = await _userRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            var claims = await _userRepository.GetClaimsAsync(userEntity);
            var claimsDto = _mapper.Map<IEnumerable<ClaimDto>>(claims);

            return Ok(claimsDto);
        }
    }
}
