using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.RequestModels.PlayLists;
using Darwin.Domain.ResponseModels.PlayLists;
using Darwin.Shared.Dtos;

namespace Darwin.Application.Features.PlayLists.Commands;

public static class AddContentsToPlayList
{
    public record Command(AddContentsToPlayListRequest Model, string creatorId) : ICommand<DarwinResponse<GetPlayListByIdResponse>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<GetPlayListByIdResponse>>
    {
        private readonly IPlayListService _playListService;

        public CommandHandler(IPlayListService playListService)
        {
            _playListService = playListService;
        }

        public async Task<DarwinResponse<GetPlayListByIdResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            return DarwinResponse<GetPlayListByIdResponse>.Success(await _playListService.AddContentsToPlayList(request.Model), 201);
        }
    }
}
