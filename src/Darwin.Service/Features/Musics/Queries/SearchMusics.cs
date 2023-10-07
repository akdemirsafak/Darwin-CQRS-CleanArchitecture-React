using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Musics.Queries;

public static class SearchMusics
{
    public record Query(string SearchText) : IQuery<DarwinResponse<List<SearchMusicResponse>>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<SearchMusicResponse>>>
    {
        private readonly IGenericRepository<Music> _repository;

        public QueryHandler(IGenericRepository<Music> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<SearchMusicResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var existMusics = await _repository.GetAllAsync(x =>
            x.Name.ToLower().TrimStart().TrimEnd().Contains(request.SearchText.ToLower().TrimStart().TrimEnd()));
            return DarwinResponse<List<SearchMusicResponse>>.Success(existMusics.Adapt<List<SearchMusicResponse>>());
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


