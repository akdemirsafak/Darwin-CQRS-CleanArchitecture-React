using Darwin.Core.BaseDto;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Queries;

public static class GetPlayListById
{
    public record Query(Guid id) : IQuery<DarwinResponse<GetPlayListByIdResponse>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetPlayListByIdResponse>>
    {
        private readonly IPlayListRepository _playListRepository;
        public QueryHandler(IPlayListRepository playListRepository)
        {

            _playListRepository = playListRepository;
        }

        public async Task<DarwinResponse<GetPlayListByIdResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var playList=await _playListRepository.GetPlayListByIdWithContentsAsync(request.id);
            if (playList is null)
                return DarwinResponse<GetPlayListByIdResponse>.Fail("Çalma listesi bulunamadı.", 404);

            return DarwinResponse<GetPlayListByIdResponse>.Success(playList.Adapt<GetPlayListByIdResponse>());

        }
    }
}
