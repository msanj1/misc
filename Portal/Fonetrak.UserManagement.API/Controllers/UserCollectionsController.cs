using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.Models;
using Fonetrak.UserManagement.API.Services;
using Fonetrak.Web.Common.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace Fonetrak.UserManagement.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/usercollections")]
    [ApiController]
    public class UserCollectionsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserCollectionsController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("({ids})", Name = "GetUserCollection")]
        public IActionResult GetUserCollection(
            [ModelBinder(binderType:typeof(ArrayModelBinder))] IEnumerable<string> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }
            var userIds = ids.ToList();

            var userEntities = _userRepository.GetUsers(userIds).ToList();
            if (userEntities.Count != userIds.Count)
            {
                return NotFound();
            }

            var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(userEntities);
            return Ok(usersToReturn);
        }

        //[HttpPost]
        //public async Task <ActionResult<IEnumerable<UserDto>>> CreateUserCollection(
        //    IEnumerable<UserForRegistrationDto> userCollection)
        //{
        //    var possibleErrors = new List<dynamic>();
        //    List<ApplicationUser> usersCreated = new List<ApplicationUser>();
        //    foreach (var user in userCollection)
        //    {
        //        var usersToCreate = _mapper.Map<ApplicationUser>(user);
        //        var claimsToCreate = _mapper.Map<List<Claim>>(user.Claims);
        //        var errors = await _userRepository.AddUser(usersToCreate, claimsToCreate, user.Password);

        //        foreach (var error in errors)
        //        {
        //            possibleErrors.Add(new { EntityAffected = user, Error = error });
        //        }

        //        usersCreated.Add(usersToCreate);
        //    }

        //    if (possibleErrors.Count > 0)
        //    {
        //        foreach (dynamic validationError in possibleErrors)
        //        {
        //            ModelState.AddModelError($"{validationError.EntityAffected.UserName} - {validationError.EntityAffected.Email}", 
        //                validationError.Error);
        //        }

        //        return ValidationProblem(ModelState);
        //    }

        //    var idsAsString = string.Join(",", usersCreated.Select(u => u.Id));
        //    return CreatedAtRoute("GetUserCollection",
        //        new { ids = idsAsString },
        //        usersCreated);
        //}
    }
}
