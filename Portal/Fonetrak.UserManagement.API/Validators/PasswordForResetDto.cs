using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Fonetrak.UserManagement.API.Models;

namespace Fonetrak.UserManagement.API.Validators
{
    public class PasswordForResetDtoValidator : AbstractValidator<PasswordForResetDto>
    {
        public PasswordForResetDtoValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(x => x.Password)
                .Equal(x => x.ConfirmPassword)
                .When(x => !string.IsNullOrWhiteSpace(x.Password))
                .WithMessage("Confirm Password is different to Password");
        }
    }
}
