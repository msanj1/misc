using System;
using FluentValidation;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Fonetrak.UserManagement.API.Validators
{
    public class UserForManipulationDtoValidator<T> : AbstractValidator<T>
    where T: UserForManipulationDto
    {
        public UserForManipulationDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();


        }
    }
}