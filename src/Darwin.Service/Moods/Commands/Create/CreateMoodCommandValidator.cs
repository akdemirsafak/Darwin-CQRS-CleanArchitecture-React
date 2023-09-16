using FluentValidation;

namespace Darwin.Service.Moods.Commands.Create;

public class CreateMoodCommandValidator : AbstractValidator<CreateMoodCommand>
{
    public CreateMoodCommandValidator()
    {
        RuleFor(x=>x.Model.Name)
            .NotNull()
            .NotEmpty()
            .Length(3, 64);
    }
}
