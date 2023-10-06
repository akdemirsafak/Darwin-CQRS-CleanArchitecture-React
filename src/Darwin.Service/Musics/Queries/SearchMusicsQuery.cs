using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Musics.Queries;

public class SearchMusicsQuery : IQuery<DarwinResponse<List<SearchMusicResponse>>>
{
    public string SearchText { get; }

    public SearchMusicsQuery(string searchText)
    {
        SearchText = searchText;
    }
    public class Handler : IQueryHandler<SearchMusicsQuery, DarwinResponse<List<SearchMusicResponse>>>
    {
        private readonly IGenericRepository<Music> _repository;


        public Handler(IGenericRepository<Music> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<SearchMusicResponse>>> Handle(SearchMusicsQuery request, CancellationToken cancellationToken)
        {
            var existMusics= await _repository.GetAllAsync(x=>
            x.Name.ToLower().TrimStart().TrimEnd().Contains(request.SearchText.ToLower().TrimStart().TrimEnd()));
            return DarwinResponse<List<SearchMusicResponse>>.Success(existMusics.Adapt<List<SearchMusicResponse>>());
        }
    }
}
