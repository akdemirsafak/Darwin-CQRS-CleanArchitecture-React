using FluentValidation;

namespace Darwin.Service.Categories.Commands.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x=>x.Model.Name).NotEmpty().NotNull().Length(3,64);
        RuleFor(x => x.Model.ImageUrl).NotEmpty().NotNull();

    }
}
