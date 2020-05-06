﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.Models;
using Fonetrak.UserManagement.API.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
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

        [HttpGet]
        public IActionResult GetUsers()
        {
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(_userRepository.GetUsers());
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
            
            var possibleErrors = await _userRepository.AddUser(userEntity, claimEntities, user.Password);
            if (possibleErrors.Count > 0)
            {
                foreach (var validationError in possibleErrors)
                {
                    ModelState.AddModelError("", validationError);
                }

                return ValidationProblem(ModelState);
            }

            _userRepository.Save();

            var userDto = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("GetUser", new { userId = userEntity.Id }, userDto);
        }
    }
}