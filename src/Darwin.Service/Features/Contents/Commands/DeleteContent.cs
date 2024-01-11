using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Common;
using Darwin.Service.Common;
using FluentValidation;

namespace Darwin.Service.Features.Contents.Commands;

public static class DeleteContent
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler(IGenericRepository<Content> _contentRepository) 
        : ICommandHandler<Command, DarwinResponse<NoContent>>
    {

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existContent = await _contentRepository.GetAsync(x => x.Id == request.Id);
            if (existContent == null)
                return DarwinResponse<NoContent>.Fail("");

            existContent.IsUsable = false;

            await _contentRepository.UpdateAsync(existContent);

            return DarwinResponse<NoContent>.Success(204);
        }
    }


    public class DeleteContentCommandValidator : AbstractValidator<Command>
    {
        public DeleteContentCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}

