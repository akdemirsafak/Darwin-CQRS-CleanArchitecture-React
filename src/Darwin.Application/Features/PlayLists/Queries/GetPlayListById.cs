using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.ResponseModels.PlayLists;
using Darwin.Shared.Dtos;

namespace Darwin.Application.Features.PlayLists.Queries;

public static class GetPlayListById
{
    public record Query(Guid id) : IQuery<DarwinResponse<GetPlayListByIdResponse>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetPlayListByIdResponse>>
    {
        private readonly IPlayListService _playListService;

        public QueryHandler(IPlayListService playListService)
        {
            _playListService = playListService;
        }


        public async Task<DarwinResponse<GetPlayListByIdResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            return DarwinResponse<GetPlayListByIdResponse>.Success(await _playListService.GetByIdAsync(request.id));

        }
    }
}
