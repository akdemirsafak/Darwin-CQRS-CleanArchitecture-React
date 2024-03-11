using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;
using FluentValidation;

namespace Darwin.Application.Features.Contents.Commands;

public static class DeleteContent
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler(IContentService _contentService)
        : ICommandHandler<Command, DarwinResponse<NoContent>>
    {

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _contentService.DeleteAsync(request.Id);
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

