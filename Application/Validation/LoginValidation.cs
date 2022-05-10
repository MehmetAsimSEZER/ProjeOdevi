using Application.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class LoginValidation : AbstractValidator<LoginDTO>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a UserName");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Enter a Password");
        }
    }
}
