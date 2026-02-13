using FluentValidation;
using Planner.API.Models;

namespace Planner.API.Validators;

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required")
            .Length(2, 50).WithMessage("UserName must be between 2 and 50 characters");
    }
}
