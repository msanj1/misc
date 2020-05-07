using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.Models;
using Fonetrak.UserManagement.API.ResourceParameters;
using Fonetrak.UserManagement.API.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Fonetrak.UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetUsers")]
        public IActionResult GetUsers([FromQuery] UsersResourceParameters parameters)
        {
            var usersToReturn = _userRepository.GetUsers(parameters);
            var paginationMetadata = new
            {
                totalCount = usersToReturn.TotalCount,
                pageSize = usersToReturn.PageSize,
                currentPage = usersToReturn.CurrentPage,
                totalPages = usersToReturn.TotalPages,
                perviousPage = usersToReturn.HasPrevious ? Url.Link("GetUsers",
                    new
                    {
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize
                    }) : null,
                nextPage = usersToReturn.HasNext ? Url.Link("GetUsers",
                    new
                    {
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize
                    }) : null
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersToReturn);
            return Ok(usersDto);
        }

        [HttpGet("{userId}",Name = "GetUser")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var userEntity = await _userRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(userEntity);

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterUser(UserForRegistrationDto user)
        {
            var userEntity = _mapper.Map<ApplicationUser>(user);
            var claimEntities = _mapper.Map<List<Claim>>(user.Claims);
            
            var possibleErrors = await _userRepository.AddUserAsync(userEntity, claimEntities, user.Password);
            if (possibleErrors.Count > 0)
            {
                foreach (var validationError in possibleErrors)
                {
                    ModelState.AddModelError("", validationError);
                }

                return ValidationProblem(ModelState);
            }

            await _userRepository.SaveAsync();

            var userDto = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("GetUser", new { userId = userEntity.Id }, userDto);
        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> PartiallyUpdateUser(string userId,
            JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            var userEntity = await _userRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            var userClaimsEntity = await _userRepository.GetClaimsAsync(userEntity);

            var userToUpdateDto = _mapper.Map<UserForUpdateDto>(userEntity);
            userToUpdateDto.Claims = _mapper.Map<List<ClaimForUpdateDto>>(userClaimsEntity);

            patchDocument.ApplyTo(userToUpdateDto,ModelState);

            if (!TryValidateModel(!TryValidateModel(userToUpdateDto)))
            {
                return ValidationProblem(ModelState);
            }

            var userToUpdate = _mapper.Map(userToUpdateDto, userEntity);
            var claimsToUpdate = _mapper.Map<List<Claim>>(userToUpdateDto.Claims);

            var possibleErrors = await _userRepository.UpdateUserAsync(userToUpdate,claimsToUpdate);
            if (possibleErrors.Count > 0)
            {
                foreach (var validationError in possibleErrors)
                {
                    ModelState.AddModelError("", validationError);
                }

                return ValidationProblem(ModelState);
            }

            await _userRepository.SaveAsync();

            var userDto = _mapper.Map<UserDto>(userToUpdate);

            return CreatedAtRoute("GetUser", new { userId = userEntity.Id }, userDto);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var userEntity = await _userRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            var possibleErrors = await _userRepository.DeleteUserAsync(userEntity);
            if (possibleErrors.Count > 0)
            {
                foreach (var validationError in possibleErrors)
                {
                    ModelState.AddModelError("", validationError);
                }

                return ValidationProblem(ModelState);
            }

            await _userRepository.SaveAsync();

            return NoContent();
        }

    }
}
