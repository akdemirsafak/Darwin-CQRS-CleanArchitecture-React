using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;

namespace Darwin.Service.Moods.Queries;

public class GetMoodsQuery:IQuery<DarwinResponse<List<GetMoodResponse>>>
{
    public class Handler : IQueryHandler<GetMoodsQuery, DarwinResponse<List<GetMoodResponse>>>
    {
        private readonly IGenericRepositoryAsync<Mood> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Mood> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<List<GetMoodResponse>>> Handle(GetMoodsQuery request, CancellationToken cancellationToken)
        {
            var moods= await _repository.GetAllAsync();
            if (moods is null)
            {
                return DarwinResponse<List<GetMoodResponse>>.Fail("NotFound.");
            }
            return DarwinResponse<List<GetMoodResponse>>.Success(_mapper.Map<List<GetMoodResponse>>(moods));
        }
    }
}
