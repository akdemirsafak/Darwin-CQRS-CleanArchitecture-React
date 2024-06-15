using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Mood;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;

namespace Darwin.Application.Features.Moods.Queries;

public static class GetMoods
{
    public record Query() : IQuery<DarwinResponse<List<GetMoodResponse>>>;
    //    , ICacheableQuery
    //{
    //    public string CachingKey => "GetAllMoods";

    //    public double CacheTime => 0.5; //Minutes - Dakika cinsinden.
    //}

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetMoodResponse>>>
    {
        private readonly IMoodService _moodService;

        public QueryHandler(IMoodService moodService)
        {
            _moodService = moodService;
        }

        public async Task<DarwinResponse<List<GetMoodResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return DarwinResponse<List<GetMoodResponse>>.Success(await _moodService.GetAllAsync());
        }
    }
}
