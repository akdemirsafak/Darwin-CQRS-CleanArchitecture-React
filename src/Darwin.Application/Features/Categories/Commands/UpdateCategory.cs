using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.RequestModels.Categories;
using Darwin.Domain.ResponseModels.Categories;
using Darwin.Shared.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Categories.Commands;

public static class UpdateCategory
{
    public record Command(Guid Id, UpdateCategoryRequest Model) : ICommand<DarwinResponse<UpdatedCategoryResponse>>;

    public class CommandHandler(ICategoryService _categoryService) : ICommandHandler<Command, DarwinResponse<UpdatedCategoryResponse>>
    {
        public async Task<DarwinResponse<UpdatedCategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var updatedCategoryResponse= await _categoryService.UpdateAsync(request.Id,request.Model);
            return DarwinResponse<UpdatedCategoryResponse>.Success(updatedCategoryResponse);
        }
    }
    public class UpdateCategoryCommandValidator : AbstractValidator<Command>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
        }
    }
}


