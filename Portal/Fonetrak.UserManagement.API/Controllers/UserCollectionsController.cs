using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fonetrak.UserManagement.API.Models;
using Fonetrak.UserManagement.API.Services;
using Fonetrak.Web.Common.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace Fonetrak.UserManagement.API.Controllers
{
    [Route("api/usercollections")]
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
    }
}
