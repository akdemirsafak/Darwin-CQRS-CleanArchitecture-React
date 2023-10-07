using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.Categories.Queries;

public static class GetCategories
{
    public record Query() : IQuery<DarwinResponse<List<GetCategoryResponse>>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetCategoryResponse>>>
    {
        private readonly IGenericRepository<Category> _repository;

        public QueryHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<GetCategoryResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync();

            return DarwinResponse<List<GetCategoryResponse>>.Success(categories.Adapt<List<GetCategoryResponse>>(), 200);

        }
    }
}

