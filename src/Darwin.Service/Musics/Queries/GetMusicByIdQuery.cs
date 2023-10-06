using Darwin.Core.BaseDto;
using Darwin.Infrastructure;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Service.Musics.Queries;

public class GetMusicByIdQuery : IQuery<DarwinResponse<GetMusicByIdResponse>>
{
    public Guid Id { get; }

    public GetMusicByIdQuery(Guid id)
    {
        Id = id;
    }

    public class Handler : IQueryHandler<GetMusicByIdQuery, DarwinResponse<GetMusicByIdResponse>>
    {
        private readonly DarwinDbContext _dbContext;


        public Handler(DarwinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DarwinResponse<GetMusicByIdResponse>> Handle(GetMusicByIdQuery request, CancellationToken cancellationToken)
        {
            var music= await _dbContext.Musics.Include(x=>x.Moods).Include(x=>x.Categories).SingleOrDefaultAsync(x=>x.Id==request.Id);

            return DarwinResponse<GetMusicByIdResponse>.Success(music.Adapt<GetMusicByIdResponse>());
        }
    }
}
