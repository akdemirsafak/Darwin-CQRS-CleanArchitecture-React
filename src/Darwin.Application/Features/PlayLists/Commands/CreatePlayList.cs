using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.RequestModels.PlayLists;
using Darwin.Domain.ResponseModels.PlayLists;
using FluentValidation;

namespace Darwin.Application.Features.PlayLists.Commands;

public static class CreatePlayList
{
    public record Command(CreatePlayListRequest Model, string creatorId) : ICommand<DarwinResponse<CreatedPlayListResponse>>;

    public class CommandHandler(IPlayListService _playListService)
        : ICommandHandler<Command, DarwinResponse<CreatedPlayListResponse>>
    {

        public async Task<DarwinResponse<CreatedPlayListResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            return DarwinResponse<CreatedPlayListResponse>.Success(await _playListService.CreateAsync(request.Model, request.creatorId), 201);
        }
    }
    public class CreateListValidator : AbstractValidator<Command>
    {
        public CreateListValidator()
        {
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 64);
        }
    }
}
