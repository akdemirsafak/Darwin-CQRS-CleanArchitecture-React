using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using FluentValidation;


namespace Darwin.Contents.Service.Features.Categories.Commands;
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
