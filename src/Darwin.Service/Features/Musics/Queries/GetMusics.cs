using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using Darwin.Service.UserHelper;
using Mapster;

namespace Darwin.Service.Features.Musics.Queries;

public static class GetMusics
{
    public record Query() : IQuery<DarwinResponse<List<GetMusicResponse>>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetMusicResponse>>>
    {
        private readonly IGenericRepository<Music> _repository;
        private readonly ICurrentUser _currentUser;

        public QueryHandler(IGenericRepository<Music> repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<DarwinResponse<List<GetMusicResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var musics = await _repository.GetAllAsync(x=>x.AgeRate.Rate<_currentUser.UserAge);
            return DarwinResponse<List<GetMusicResponse>>.Success(musics.Adapt<List<GetMusicResponse>>(), 200);

        }
    }
}
