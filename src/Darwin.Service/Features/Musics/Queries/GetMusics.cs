using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.Musics.Queries;

public static class GetMusics
{
    public record Query() : IQuery<DarwinResponse<List<GetMusicResponse>>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetMusicResponse>>>
    {
        private readonly IGenericRepository<Music> _repository;

        public QueryHandler(IGenericRepository<Music> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<GetMusicResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var musics = await _repository.GetAllAsync();
            return DarwinResponse<List<GetMusicResponse>>.Success(musics.Adapt<List<GetMusicResponse>>(), 200);

        }
    }
}
