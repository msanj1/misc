using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fonetrak.UserManagement.API.Models
{
    public class UserForRegistrationDto : UserForManipulationDto
    {
        public List<ClaimForRegistrationDto> Claims { get; set; } = new List<ClaimForRegistrationDto>();

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
