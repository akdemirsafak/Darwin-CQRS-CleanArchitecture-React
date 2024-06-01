using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.RequestModels;
using Darwin.Domain.ResponseModels.Categories;
using Darwin.Shared.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Categories.Queries;

public static class GetCategoryList
{
    public record Query(GetPaginationListRequest Model) : IQuery<DarwinResponse<GetCategoryListResponse>>, ICacheableQuery
    {
        public string CachingKey => "CategoryListCached";
        public double CacheTime => 2;
    }

    public class QueryHandler(ICategoryService _categoryService) : IQueryHandler<Query, DarwinResponse<GetCategoryListResponse>>
    {

        public async Task<DarwinResponse<GetCategoryListResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories= await _categoryService.GetListAsync(request.Model);
            return DarwinResponse<GetCategoryListResponse>.Success(categories);
        }
        public class GetCategoryListQueryValidator : AbstractValidator<Query>
        {
            public GetCategoryListQueryValidator()
            {
                RuleFor(x => x.Model.Page).GreaterThanOrEqualTo(1);
                RuleFor(x => x.Model.PageSize).GreaterThanOrEqualTo(2);
            }
        }
    }
}
