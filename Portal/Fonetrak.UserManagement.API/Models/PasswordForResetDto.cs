using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fonetrak.UserManagement.API.Models
{
    public class PasswordForResetDto
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
