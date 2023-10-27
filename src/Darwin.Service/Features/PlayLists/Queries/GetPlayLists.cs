using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Queries;

public static class GetPlayLists
{
    public record Query() : IQuery<DarwinResponse<List<GetPlayListResponse>>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetPlayListResponse>>>
    {
        private readonly IGenericRepository<PlayList> _playListRepository;

        public QueryHandler(IGenericRepository<PlayList> playListRepository)
        {
            _playListRepository = playListRepository;
        }

        public async Task<DarwinResponse<List<GetPlayListResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var playLists= await _playListRepository.GetAllAsync();
            return DarwinResponse<List<GetPlayListResponse>>.Success(playLists.Adapt<List<GetPlayListResponse>>());
        }
    }
}
