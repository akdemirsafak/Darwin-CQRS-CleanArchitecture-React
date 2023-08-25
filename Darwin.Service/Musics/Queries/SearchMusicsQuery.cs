using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;

namespace Darwin.Service.Musics.Queries;

public class SearchMusicsQuery:IQuery<DarwinResponse<List<SearchMusicResponse>>>
{
    public string SearchText { get; }

    public SearchMusicsQuery(string searchText)
    {
        SearchText = searchText;
    }
    public class Handler : IQueryHandler<SearchMusicsQuery, DarwinResponse<List<SearchMusicResponse>>>
    {
        private readonly IGenericRepositoryAsync<Music> _repository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IGenericRepositoryAsync<Music> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DarwinResponse<List<SearchMusicResponse>>> Handle(SearchMusicsQuery request, CancellationToken cancellationToken)
        {
            var existMusics= await _repository.GetAllAsync(x=>
            x.Name.ToLower().TrimStart().TrimEnd().Contains(request.SearchText.ToLower().TrimStart().TrimEnd()) ||
            x.Publishers.ToLower().TrimStart().TrimEnd().Contains(request.SearchText.ToLower().TrimStart().TrimEnd()));
            return DarwinResponse<List<SearchMusicResponse>>.Success(_mapper.Map<List<SearchMusicResponse>>(existMusics));
        }
    }
}
