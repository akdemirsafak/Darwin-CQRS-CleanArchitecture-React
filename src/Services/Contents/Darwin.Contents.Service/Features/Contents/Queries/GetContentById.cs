using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Content;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Contents.Queries;

public static class GetContentById
{
    public record Query(Guid Id) : IQuery<DarwinResponse<GetContentByIdResponse>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetContentByIdResponse>>
    {
        private readonly IContentService _contentService;

        public QueryHandler(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async Task<DarwinResponse<GetContentByIdResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var getContentByIdResponse = await _contentService.GetByIdAsync(request.Id);

            return DarwinResponse<GetContentByIdResponse>.Success(getContentByIdResponse);
        }
    }

    public class GetMusicByIdQueryValidator : AbstractValidator<Query>
    {
        public GetMusicByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}

