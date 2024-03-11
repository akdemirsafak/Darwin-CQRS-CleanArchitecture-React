using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.ResponseModels.PlayLists;

namespace Darwin.Application.Features.PlayLists.Queries;

public static class GetPlayLists
{
    public record Query() : IQuery<DarwinResponse<List<GetPlayListResponse>>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetPlayListResponse>>>
    {
        private readonly IPlayListService _playListService;

        public QueryHandler(IPlayListService playListService)
        {
            _playListService = playListService;
        }

        public async Task<DarwinResponse<List<GetPlayListResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var playLists= await _playListService.GetAllAsync();
            return DarwinResponse<List<GetPlayListResponse>>.Success(playLists);
        }
    }
}
