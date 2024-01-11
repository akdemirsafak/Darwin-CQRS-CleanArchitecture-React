using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request;
using Darwin.Model.Response.Contents;
using Darwin.Service.Common;
using Darwin.Service.Helper;
using Darwin.Service.Services;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Contents.Queries;

public static class GetContentList
{
    public record Query(GetPaginationListRequest Model) : IQuery<DarwinResponse<GetContentListResponse>>, ICacheableQuery
    {
        public string CachingKey => "GetContentList";

        public double CacheTime => 0.5;
    }

    public class QueryHandler(IGenericRepository<Content> _contentRepository) : IQueryHandler<Query, DarwinResponse<GetContentListResponse>>
    {
        public async Task<DarwinResponse<GetContentListResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var queryable=_contentRepository.GetList();
            Paginate<Content> paginate= Paginate<Content>.ToPagedList(queryable,request.Model.Page,request.Model.PageSize);

            return DarwinResponse<GetContentListResponse>.Success(paginate.Adapt<GetContentListResponse>());
        }
    }
    public class GetPaginableContentQueryValidator : AbstractValidator<Query>
    {
        public GetPaginableContentQueryValidator()
        {
            RuleFor(x => x.Model.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Model.PageSize).GreaterThanOrEqualTo(2);
        }
    }
}
