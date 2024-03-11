using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Dtos.Youtube;

namespace Darwin.Application.Features.Youtube;


public static class GetTheWeekndChannel
{
    public record Command(string PageToken, int Size) : ICommand<DarwinResponse<List<YoutubeApiResponse>>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<List<YoutubeApiResponse>>>
    {
        private readonly IYoutubeService _youtubeService;

        public CommandHandler(IYoutubeService youtubeService)
        {
            _youtubeService = youtubeService;
        }

        public async Task<DarwinResponse<List<YoutubeApiResponse>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var contentList=await _youtubeService.GetChannelContentsAsync(request.PageToken,request.Size);

            return DarwinResponse<List<YoutubeApiResponse>>.Success(contentList, 200);

        }
    }
}
