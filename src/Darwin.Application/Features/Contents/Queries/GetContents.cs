using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.ResponseModels.Contents;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.Contents.Queries;

public static class GetContents
{
    public record Query() : IQuery<DarwinResponse<List<GetContentResponse>>>, ICacheableQuery
    {
        public string CachingKey => "GetContents";
        public double CacheTime => 0.5;
    }

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetContentResponse>>>
    {
        private readonly IContentService _contentService;

        public QueryHandler(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async Task<DarwinResponse<List<GetContentResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var getContentsResponse = await _contentService.GetAllAsync();

            return DarwinResponse<List<GetContentResponse>>.Success(getContentsResponse);
        }
    }
}
