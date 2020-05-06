using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fonetrak.UserManagement.API.Models
{
    public class UserForRegistrationDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public List<ClaimForRegistrationDto> Claims { get; set; } = new List<ClaimForRegistrationDto>();
    }
}
