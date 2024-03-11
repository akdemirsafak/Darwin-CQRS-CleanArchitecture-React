using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.RequestModels;
using Darwin.Domain.ResponseModels.Contents;
using FluentValidation;

namespace Darwin.Application.Features.Contents.Queries;

public static class GetContentList
{
    public record Query(GetPaginationListRequest Model) : IQuery<DarwinResponse<GetContentListResponse>>, ICacheableQuery
    {
        public string CachingKey => "GetContentList";

        public double CacheTime => 0.5;
    }

    public class QueryHandler(IContentService _contentService) : IQueryHandler<Query, DarwinResponse<GetContentListResponse>>
    {
        public async Task<DarwinResponse<GetContentListResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            return DarwinResponse<GetContentListResponse>.Success(await _contentService.GetListAsync(request.Model));
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
