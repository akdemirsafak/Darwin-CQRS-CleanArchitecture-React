using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.RequestModels.PlayLists;
using Darwin.Domain.ResponseModels.PlayLists;

namespace Darwin.Application.Features.PlayLists.Commands
{
    public static class RemoveContentsFromPlayList
    {
        public record Command(RemoveContentsFromPlayListRequest Model, string _currentUserId) : ICommand<DarwinResponse<GetPlayListByIdResponse>>;
        public class CommandHandler : ICommandHandler<Command, DarwinResponse<GetPlayListByIdResponse>>
        {
            private readonly IPlayListService _playListService;

            public CommandHandler(IPlayListService playListService)
            {
                _playListService = playListService;
            }

            public async Task<DarwinResponse<GetPlayListByIdResponse>> Handle(Command request, CancellationToken cancellationToken)
            {

                var getPlayListByIdResponse= await _playListService.RemoveContentsFromPlayList(request.Model);
                return DarwinResponse<GetPlayListByIdResponse>.Success(getPlayListByIdResponse, 200);
            }
        }
    }
}