using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Common;
using Darwin.Service.Common;
using FluentValidation;

namespace Darwin.Service.Features.Categories.Commands;

public static class DeleteCategory
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContent>>;
    public class CommandHandler(IGenericRepository<Category> _repository) : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existMusic = await _repository.GetAsync(x => x.Id == request.Id);
            if (existMusic == null)
                return DarwinResponse<NoContent>.Fail("");

            existMusic.IsUsable = false;
            await _repository.UpdateAsync(existMusic);

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
