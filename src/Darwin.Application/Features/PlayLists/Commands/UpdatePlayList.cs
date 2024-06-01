using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.RequestModels.PlayLists;
using Darwin.Domain.ResponseModels.PlayLists;
using Darwin.Shared.Dtos;

namespace Darwin.Application.Features.PlayLists.Commands;

public static class UpdatePlayList
{
    public record Command(Guid id, UpdatePlayListRequest Model, string creatorId) : ICommand<DarwinResponse<UpdatedPlayListResponse>>;

    public class CommandHandler(IPlayListService _playListService)
        : ICommandHandler<Command, DarwinResponse<UpdatedPlayListResponse>>
    {
        public async Task<DarwinResponse<UpdatedPlayListResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var updatedPlayListResponse=await _playListService.UpdateAsync(request.id,request.Model,request.creatorId);

            return DarwinResponse<UpdatedPlayListResponse>.Success(updatedPlayListResponse);
        }
    }
}
