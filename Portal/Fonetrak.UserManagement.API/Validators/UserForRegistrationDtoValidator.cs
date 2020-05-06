using System;
using FluentValidation;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Fonetrak.UserManagement.API.Validators
{
    public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password)
                .Equal(x => x.ConfirmPassword)
                .When(x => !string.IsNullOrWhiteSpace(x.Password))
                .WithMessage("Confirm Password is different to Password");

            RuleForEach(x => x.Claims)
                .Must(c =>
                    !string.IsNullOrWhiteSpace(c.Type) && !string.IsNullOrWhiteSpace(c.Value));

        }
    }
}