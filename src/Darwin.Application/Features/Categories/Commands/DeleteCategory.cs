using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;
using FluentValidation;

namespace Darwin.Application.Features.Categories.Commands;

public static class DeleteCategory
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContent>>;
    public class CommandHandler(ICategoryService _categoryService) : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(request.Id);

            return DarwinResponse<NoContent>.Success(204);
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
