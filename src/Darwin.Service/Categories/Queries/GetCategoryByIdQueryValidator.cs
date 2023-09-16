using FluentValidation;

namespace Darwin.Service.Categories.Queries;

public class GetCategoryByIdQueryValidator:AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}
