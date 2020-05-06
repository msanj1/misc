using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fonetrak.UserManagement.API.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        //public List<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
    }
}
