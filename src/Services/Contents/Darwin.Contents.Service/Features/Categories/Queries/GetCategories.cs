using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Category;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;


namespace Darwin.Contents.Service.Features.Categories.Queries;

public static class GetCategories
{
    public record Query : IQuery<DarwinResponse<List<GetCategoryResponse>>>;

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

