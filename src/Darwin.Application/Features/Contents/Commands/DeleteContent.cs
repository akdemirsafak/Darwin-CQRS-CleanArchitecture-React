using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Share.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Contents.Commands;

public static class DeleteContent
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContentDto>>;

    public class CommandHandler(IContentService _contentService)
        : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _contentService.DeleteAsync(request.Id);
            return DarwinResponse<NoContentDto>.Success(204);
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

