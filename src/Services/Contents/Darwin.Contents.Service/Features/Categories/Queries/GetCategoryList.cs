using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Category;
using Darwin.Contents.Core.RequestModels;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using FluentValidation;


namespace Darwin.Contents.Service.Features.Categories.Queries;

public static class GetCategoryList
{
    public record Query(GetPaginationListRequest Model) : IQuery<DarwinResponse<GetCategoryListResponse>>
    //, ICacheableQuery
    {
        //public string CachingKey => "CategoryListCached";
        //public double CacheTime => 2;
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
