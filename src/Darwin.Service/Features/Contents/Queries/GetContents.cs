using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Contents;
using Darwin.Service.Common;
using Darwin.Service.Helper;
using Mapster;

namespace Darwin.Service.Features.Contents.Queries;

public static class GetContents
{
    public record Query() : IQuery<DarwinResponse<List<GetContentResponse>>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetContentResponse>>>
    {
        private readonly IGenericRepository<Content> _repository;
        private readonly ICurrentUser _currentUser;

        public QueryHandler(IGenericRepository<Content> repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<DarwinResponse<List<GetContentResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var musics = await _repository.GetAllAsync(x=>x.AgeRate.Rate<_currentUser.UserAge);
            return DarwinResponse<List<GetContentResponse>>.Success(musics.Adapt<List<GetContentResponse>>());

        }
    }
}
