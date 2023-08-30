using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;

namespace Darwin.Service.Musics.Queries;

public class GetMusicsQuery : IQuery<DarwinResponse<List<GetMusicResponse>>>
{

    public class Handler : IQueryHandler<GetMusicsQuery, DarwinResponse<List<GetMusicResponse>>>
    {
        private readonly IGenericRepositoryAsync<Music> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Music> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<List<GetMusicResponse>>> Handle(GetMusicsQuery request, CancellationToken cancellationToken)
        {
            var musics = await _repository.GetAllAsync();
            return DarwinResponse<List<GetMusicResponse>>.Success(_mapper.Map<List<GetMusicResponse>>(musics), 200);

        }
    }
}
