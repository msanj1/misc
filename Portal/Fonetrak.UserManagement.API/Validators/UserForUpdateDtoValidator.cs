using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Fonetrak.UserManagement.API.Models;

namespace Fonetrak.UserManagement.API.Validators
{
    public class UserForUpdateDtoValidator :
        UserForManipulationDtoValidator<UserForUpdateDto>
    {
        public UserForUpdateDtoValidator()
        {
            RuleForEach(x => x.Claims)
                .Must(c =>
                    !string.IsNullOrWhiteSpace(c.Type) && !string.IsNullOrWhiteSpace(c.Value));
        }
    }
}
