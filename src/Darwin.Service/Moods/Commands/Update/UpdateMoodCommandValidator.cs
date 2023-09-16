using FluentValidation;

namespace Darwin.Service.Moods.Commands.Update;
public class UpdateMoodCommandValidator : AbstractValidator<UpdateMoodCommand>
{
    public UpdateMoodCommandValidator()
    {
        RuleFor(x => x.Model.Name)
            .NotNull()
            .NotEmpty()
            .Length(3, 64);
    }
}