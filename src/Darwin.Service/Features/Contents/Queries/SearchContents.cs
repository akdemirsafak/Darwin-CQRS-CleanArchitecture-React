using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Contents;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Contents.Queries;

public static class SearchContents
{
    public record Query(string SearchText) : IQuery<DarwinResponse<List<SearchContentResponse>>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<SearchContentResponse>>>
    {
        private readonly IGenericRepository<Content> _repository;

        public QueryHandler(IGenericRepository<Content> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<SearchContentResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var existMusics = await _repository.GetAllAsync(x =>
            x.Name.Contains(request.SearchText));
            return DarwinResponse<List<SearchContentResponse>>.Success(existMusics.Adapt<List<SearchContentResponse>>());
        }
    }

    public class SearchMusicsQueryValidator : AbstractValidator<Query>
    {
        public SearchMusicsQueryValidator()
        {
            RuleFor(x => x.SearchText)
                .NotNull()
                .NotEmpty()
                .Length(3, 25);
        }
    }
}


