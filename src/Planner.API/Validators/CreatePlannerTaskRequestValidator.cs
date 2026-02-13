using FluentValidation;
using Planner.API.Models.Requests;

namespace Planner.API.Validators;

public class CreatePlannerTaskRequestValidator : AbstractValidator<CreatePlannerTaskRequest>
{
    public CreatePlannerTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(2000).When(x => x.Description != null);
    }
}
