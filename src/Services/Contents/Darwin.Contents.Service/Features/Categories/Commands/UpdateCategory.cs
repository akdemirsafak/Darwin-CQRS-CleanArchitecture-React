using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.RequestModels.Categories;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using FluentValidation;


namespace Darwin.Contents.Service.Features.Categories.Commands;

public static class UpdateCategory
{
    public record Command(Guid Id, UpdateCategoryRequest Model) : ICommand<DarwinResponse<NoContentDto>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        private readonly ICategoryService _categoryService;

        public CommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _categoryService.UpdateAsync(request.Id, request.Model);
            return DarwinResponse<NoContentDto>.Success(200);
        }
    }
    public class UpdateCategoryCommandValidator : AbstractValidator<Command>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 32);
        }
    }
}


