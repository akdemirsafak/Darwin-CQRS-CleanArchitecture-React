using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.RequestModels.Moods;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Moods.Commands;

public static class UpdateMood
{
    public record Command(Guid Id, UpdateMoodRequest Model) : ICommand<DarwinResponse<NoContentDto>>;

    public class CommandHandler(IMoodService _moodService)
        : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {

        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _moodService.UpdateAsync(request.Id, request.Model);
            return DarwinResponse<NoContentDto>.Success(204);
        }
    }
    public class UpdateMoodCommandValidator : AbstractValidator<Command>
    {
        public UpdateMoodCommandValidator()
        {
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 32);
        }
    }
}

