using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.ResponseModels.PlayLists;
using Darwin.Shared.Dtos;

namespace Darwin.Application.Features.PlayLists.Queries;

public static class GetMyPlayLists
{
    public record Query(string currentUserId) : IQuery<DarwinResponse<List<GetPlayListResponse>>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetPlayListResponse>>>
    {
        private readonly IPlayListService _playListService;

        public QueryHandler(IPlayListService playListService)
        {
            _playListService = playListService;
        }

        public async Task<DarwinResponse<List<GetPlayListResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var myLists= await _playListService.GetAllListsOfUserAsync(request.currentUserId);
            return DarwinResponse<List<GetPlayListResponse>>.Success(myLists);
        }
    }
}
