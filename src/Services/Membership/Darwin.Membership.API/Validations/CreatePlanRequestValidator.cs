
using Darwin.Membership.API.Models.Plan;
using FluentValidation;

namespace Darwin.Membership.API.Validations;
public sealed class CreatePlanRequestValidator : AbstractValidator<CreatePlanRequest>
{
    public CreatePlanRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name is required")
            .MinimumLength(4)
                .WithMessage("Name must be greater than 4 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
        RuleFor(x => x.Description)
            .MaximumLength(500)
                .WithMessage("Description must be less than 500 characters");
    }
}