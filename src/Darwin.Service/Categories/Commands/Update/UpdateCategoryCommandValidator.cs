using FluentValidation;

namespace Darwin.Service.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
    }
}
