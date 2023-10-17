using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.ContentAgeRates;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.AgeRates.Queries;

public static class GetContentAgeRates
{
   
    public record Query() :IQuery<DarwinResponse<List<GetContentAgeRateResponse>>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetContentAgeRateResponse>>>
    {
        private readonly IGenericRepository<ContentAgeRate> _repository;

        public QueryHandler(IGenericRepository<ContentAgeRate> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<GetContentAgeRateResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var contentAgeRates= await _repository.GetAllAsync();
            return DarwinResponse<List<GetContentAgeRateResponse>>.Success(contentAgeRates.Adapt<List<GetContentAgeRateResponse>>(),200);
        }
    }
}
