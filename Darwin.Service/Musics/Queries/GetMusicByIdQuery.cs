using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;

namespace Darwin.Service.Musics.Queries;

public class GetMusicByIdQuery:IQuery<DarwinResponse<GetMusicResponse>>
{
    public Guid Id { get; }

    public GetMusicByIdQuery(Guid id)
    {
        Id = id;
    }

    public class Handler : IQueryHandler<GetMusicByIdQuery, DarwinResponse<GetMusicResponse>>
    {
        private readonly IGenericRepositoryAsync<Music> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Music> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<GetMusicResponse>> Handle(GetMusicByIdQuery request, CancellationToken cancellationToken)
        {
            var music = await _repository.GetAsync(x=>x.Id==request.Id);
            return DarwinResponse<GetMusicResponse>.Success(_mapper.Map<GetMusicResponse>(music));
        }
    }
}
