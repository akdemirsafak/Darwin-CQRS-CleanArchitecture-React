using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.ResponseModels.Moods;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.Moods.Queries;

public static class GetMoods
{
    public record Query() : IQuery<DarwinResponse<List<GetMoodResponse>>>, ICacheableQuery
    {
        public string CachingKey => "GetAllMoods";

        public double CacheTime => 0.5; //Minutes - Dakika cinsinden.
    }

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
