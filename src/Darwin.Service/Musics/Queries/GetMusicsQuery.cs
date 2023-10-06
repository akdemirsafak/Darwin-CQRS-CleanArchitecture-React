using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Musics.Queries;

public class GetMusicsQuery : IQuery<DarwinResponse<List<GetMusicResponse>>>
{

    public class Handler : IQueryHandler<GetMusicsQuery, DarwinResponse<List<GetMusicResponse>>>
    {
        private readonly IGenericRepository<Music> _repository;

        public Handler(IGenericRepository<Music> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<GetMusicResponse>>> Handle(GetMusicsQuery request, CancellationToken cancellationToken)
        {
            var musics = await _repository.GetAllAsync();
            return DarwinResponse<List<GetMusicResponse>>.Success(musics.Adapt<List<GetMusicResponse>>(), 200);

        }
    }
}
