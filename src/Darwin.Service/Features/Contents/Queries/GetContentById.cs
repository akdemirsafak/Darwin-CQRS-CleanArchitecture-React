using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Contents;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Contents.Queries;

public static class GetContentById
{

    public record Query(Guid Id) : IQuery<DarwinResponse<GetContentByIdResponse>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetContentByIdResponse>>
    {
        private readonly IGenericRepository<Content> _contentRepository;

        public QueryHandler(IGenericRepository<Content> contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<DarwinResponse<GetContentByIdResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var content = await _contentRepository.GetAsync(x=>x.Id==request.Id);

            return DarwinResponse<GetContentByIdResponse>.Success(content.Adapt<GetContentByIdResponse>());
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

