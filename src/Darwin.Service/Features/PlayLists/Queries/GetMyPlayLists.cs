using Darwin.Core.BaseDto;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Queries;

public static class GetMyPlayLists
{
    public record Query(string currentUserId) : IQuery<DarwinResponse<List<GetPlayListResponse>>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetPlayListResponse>>>
    {
        private readonly IPlayListRepository _playListRepository;

        public QueryHandler(IPlayListRepository playListRepository)
        {
            _playListRepository = playListRepository;
        }

        public async Task<DarwinResponse<List<GetPlayListResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var myLists= await _playListRepository.GetAllAsync(x=>x.CreatorId==request.currentUserId);
            return DarwinResponse<List<GetPlayListResponse>>.Success(myLists.Adapt<List<GetPlayListResponse>>());
        }
    }
}
