using Application.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Enter a FirstName").MaximumLength(25).WithMessage("Maximum 25 character");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Enter a FirstName").MaximumLength(25).WithMessage("Maximum 25 character");

            RuleFor(x => x.Adress).NotEmpty().WithMessage("Enter a Adress");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Enter a Phone Number").MinimumLength(11).MaximumLength(11);

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a username").MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3, maximum 50 character");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a email address").EmailAddress().WithMessage("Enter a valid a email address");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Enter a password");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Enter a password").Equal(x => x.Password).WithMessage("Password don't match");
        }
    }
}
