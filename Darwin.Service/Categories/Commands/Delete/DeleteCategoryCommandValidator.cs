using FluentValidation;

namespace Darwin.Service.Categories.Commands.Delete;

public class DeleteCategoryCommandValidator:AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x=>x.Id).NotNull();
    }
}
