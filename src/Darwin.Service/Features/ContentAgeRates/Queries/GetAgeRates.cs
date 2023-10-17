using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.AgeRates;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.Moods.Queries;

public static class GetAgeRates
{
    public record Query() : IQuery<DarwinResponse<List<GetAgeRateResponse>>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetAgeRateResponse>>>
    {
        private readonly IGenericRepository<AgeRate> _repository;


        public QueryHandler(IGenericRepository<AgeRate> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<GetAgeRateResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var ageRates = await _repository.GetAllAsync();
            if (ageRates is null)
            {
                return DarwinResponse<List<GetAgeRateResponse>>.Fail("NotFound.");
            }
            return DarwinResponse<List<GetAgeRateResponse>>.Success(ageRates.Adapt<List<GetAgeRateResponse>>());
        }
    }
}
