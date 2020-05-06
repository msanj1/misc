using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fonetrak.UserManagement.API.Models
{
    public class UserForUpdateDto : UserForManipulationDto
    {
        public List<ClaimForUpdateDto> Claims { get; set; } = new List<ClaimForUpdateDto>();
        public bool Active { get; set; }

    }
}
