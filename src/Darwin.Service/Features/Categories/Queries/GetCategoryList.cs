using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using Darwin.Service.Helper;
using Darwin.Service.Services;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Categories.Queries;

public static class GetCategoryList
{
    public record Query(GetPaginationListRequest Model) : IQuery<DarwinResponse<GetCategoryListResponse>>, ICacheableQuery
    {
        public string CachingKey => "CategoryListCached";
        public double CacheTime => 2;
    }

    public class QueryHandler(IGenericRepository<Category> _categoryRepository) : IQueryHandler<Query, DarwinResponse<GetCategoryListResponse>>
    {

        public async Task<DarwinResponse<GetCategoryListResponse>> Handle(Query request, CancellationToken cancellationToken)
        {

            var queryable=_categoryRepository.GetList();
            Paginate<Category> paginate= Paginate<Category>.ToPagedList(queryable,request.Model.Page,request.Model.PageSize);

            return DarwinResponse<GetCategoryListResponse>.Success(paginate.Adapt<GetCategoryListResponse>());
        }
        public class GetCategoryListQueryValidator : AbstractValidator<Query>
        {
            public GetCategoryListQueryValidator()
            {
                RuleFor(x=>x.Model.Page).GreaterThanOrEqualTo(1);
                RuleFor(x=>x.Model.PageSize).GreaterThanOrEqualTo(2);
            }
        }
    }
}
