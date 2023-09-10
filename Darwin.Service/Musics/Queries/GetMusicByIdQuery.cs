using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Infrastructure;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Service.Musics.Queries;

public class GetMusicByIdQuery:IQuery<DarwinResponse<GetMusicByIdResponse>>
{
    public Guid Id { get; }

    public GetMusicByIdQuery(Guid id)
    {
        Id = id;
    }

    public class Handler : IQueryHandler<GetMusicByIdQuery, DarwinResponse<GetMusicByIdResponse>>
    {
        private readonly IGenericRepositoryAsync<Music> _repository;
        private readonly DarwinDbContext _dbContext;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Music> repository, IMapper mapper, DarwinDbContext dbContext)
        {
            _repository = repository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<DarwinResponse<GetMusicByIdResponse>> Handle(GetMusicByIdQuery request, CancellationToken cancellationToken)
        {
            var music= await _dbContext.Musics.Include(x=>x.Moods).Include(x=>x.Categories).SingleOrDefaultAsync(x=>x.Id==request.Id);
            //var music = await _repository.GetAsync(x=>x.Id==request.Id);
            return DarwinResponse<GetMusicByIdResponse>.Success(_mapper.Map<GetMusicByIdResponse>(music));
        }
    }
}
