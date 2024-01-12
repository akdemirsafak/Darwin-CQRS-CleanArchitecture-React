using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;
using Darwin.Service.Services;
using Mapster;

namespace Darwin.Service.Features.Moods.Queries;

public static class GetMoods
{
    public record Query() : IQuery<DarwinResponse<List<GetMoodResponse>>>, ICacheableQuery
    {
        public string CachingKey => "GetAllMoods";

        public double CacheTime => 0.5; //Minutes - Dakika cinsinden.
    }

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetMoodResponse>>>
    {
        private readonly IGenericRepository<Mood> _repository;


        public QueryHandler(IGenericRepository<Mood> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<GetMoodResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var moods = await _repository.GetAllAsync();
            if (moods is null)
            {
                return DarwinResponse<List<GetMoodResponse>>.Fail("NotFound.");
            }
            return DarwinResponse<List<GetMoodResponse>>.Success(moods.Adapt<List<GetMoodResponse>>());
        }
    }
}
