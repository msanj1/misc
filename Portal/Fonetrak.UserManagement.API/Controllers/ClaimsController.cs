using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost]
        public async Task<IActionResult> AddClaim(string userId,
            ClaimForRegistrationDto claim)
        {
            var userEntity = await _userRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            var claimToAdd = _mapper.Map<Claim>(claim);

            var possibleErrors = await _userRepository.AddClaim(userEntity, claimToAdd);
            if (possibleErrors.Count > 0)
            {
                foreach (var validationError in possibleErrors)
                {
                    ModelState.AddModelError("", validationError);
                }

                return ValidationProblem(ModelState);
            }

            var claimDto = _mapper.Map<Claim>(claimToAdd);
            return Ok(claimDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClaims(string userId)
        {
            var userEntity = await _userRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            var possibleErrors = await _userRepository.DeleteClaims(userEntity);
            if (possibleErrors.Count > 0)
            {
                foreach (var validationError in possibleErrors)
                {
                    ModelState.AddModelError("", validationError);
                }

                return ValidationProblem(ModelState);
            }

            _userRepository.Save();

            return NoContent();
        }
    }
}
