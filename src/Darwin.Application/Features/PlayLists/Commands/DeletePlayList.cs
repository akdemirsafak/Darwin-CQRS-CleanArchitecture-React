using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.ResponseModels.PlayLists;
using Darwin.Shared.Dtos;

namespace Darwin.Application.Features.PlayLists.Commands;

public static class DeletePlayList
{
    public record Command(Guid id) : ICommand<DarwinResponse<DeletedPlayListResponse>>;

    public class CommandHandler(IPlayListService _playListService)
        : ICommandHandler<Command, DarwinResponse<DeletedPlayListResponse>>
    {

        public async Task<DarwinResponse<DeletedPlayListResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _playListService.DeleteAsync(request.id);

            return DarwinResponse<DeletedPlayListResponse>.Success(204);
        }
    }
}
