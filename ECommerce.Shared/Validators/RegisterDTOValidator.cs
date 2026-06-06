using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.Validators
{
    //public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    //{
    //    public RegisterDTOValidator()
    //    {
    //        RuleFor(x => x.Email)
    //            .NotEmpty().WithMessage("Email is required.")
    //            .EmailAddress().WithMessage("Invalid email format.");

    //        RuleFor(x => x.DisplayName)
    //            .NotEmpty().WithMessage("Display name is required.");

    //        RuleFor(x => x.UserName)
    //            .NotEmpty().WithMessage("Username is required.");

    //        RuleFor(x => x.Password)
    //            .NotEmpty().WithMessage("Password is required.")
    //            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

    //        RuleFor(x => x.PhoneNumber)
    //            .NotEmpty().WithMessage("Phone number is required.")
    //            .Matches(@"^01[0125][0-9]{8}$").WithMessage("Invalid phone number.");
    //    }
    //}
}
