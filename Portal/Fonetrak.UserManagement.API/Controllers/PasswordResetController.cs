

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.Models;
using Fonetrak.UserManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fonetrak.UserManagement.API.Controllers
{
    [Route("api/users/{userId}/password_reset")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PasswordResetController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string userId
            , PasswordForResetDto passwordDetails)
        {
            var userEntity = await _userRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            var possibleErrors = await _userRepository
                .ResetPasswordAsync(userEntity, passwordDetails.Password);
            if (possibleErrors.Count > 0)
            {
                foreach (var validationError in possibleErrors)
                {
                    ModelState.AddModelError("", validationError);
                }

                return ValidationProblem(ModelState);
            }

            await _userRepository.SaveAsync();

            return Ok();
        }
    }
}
