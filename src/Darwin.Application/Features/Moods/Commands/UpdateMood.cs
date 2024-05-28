using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.RequestModels.Moods;
using Darwin.Domain.ResponseModels.Moods;
using Darwin.Share.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Moods.Commands;

public static class UpdateMood
{
    public record Command(Guid Id, UpdateMoodRequest Model) : ICommand<DarwinResponse<UpdatedMoodResponse>>;

    public class CommandHandler(IMoodService _moodService)
        : ICommandHandler<Command, DarwinResponse<UpdatedMoodResponse>>
    {

        public async Task<DarwinResponse<UpdatedMoodResponse>> Handle(Command request, CancellationToken cancellationToken)
        {

            return DarwinResponse<UpdatedMoodResponse>.Success(await _moodService.UpdateAsync(request.Id, request.Model));
        }
    }
    public class UpdateMoodCommandValidator : AbstractValidator<Command>
    {
        public UpdateMoodCommandValidator()
        {
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 64);
        }
    }
}

