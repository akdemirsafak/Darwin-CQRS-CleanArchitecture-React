using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Contents.Commands;

public static class DeleteContent
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContentDto>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        private readonly IContentService _contentService;

        public CommandHandler(IContentService contentService)
        {
            _contentService = contentService;
        }

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

