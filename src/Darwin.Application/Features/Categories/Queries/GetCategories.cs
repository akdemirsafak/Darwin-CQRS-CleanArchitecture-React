using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.ResponseModels.Categories;
using Darwin.Shared.Dtos;

namespace Darwin.Application.Features.Categories.Queries;

public static class GetCategories
{
    public record Query() : IQuery<DarwinResponse<List<GetCategoryResponse>>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetCategoryResponse>>>
    {
        private readonly ICategoryService _categoryService;

        public QueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<DarwinResponse<List<GetCategoryResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllAsync();

            return DarwinResponse<List<GetCategoryResponse>>.Success(categories, 200);

        }
    }
}

