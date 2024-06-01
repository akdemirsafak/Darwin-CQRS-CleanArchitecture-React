using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Shared.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Categories.Commands;

public static class DeleteCategory
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContentDto>>;
    public class CommandHandler(ICategoryService _categoryService) : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(request.Id);

            return DarwinResponse<NoContentDto>.Success(204);
        }
    }
    public class DeleteCategoryCommandValidator : AbstractValidator<Command>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }

}
