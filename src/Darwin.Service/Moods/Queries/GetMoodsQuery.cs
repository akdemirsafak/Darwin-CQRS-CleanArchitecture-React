using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Moods.Queries;

public class GetMoodsQuery : IQuery<DarwinResponse<List<GetMoodResponse>>>
{
    public class Handler : IQueryHandler<GetMoodsQuery, DarwinResponse<List<GetMoodResponse>>>
    {
        private readonly IGenericRepositoryAsync<Mood> _repository;


        public Handler(IGenericRepositoryAsync<Mood> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<GetMoodResponse>>> Handle(GetMoodsQuery request, CancellationToken cancellationToken)
        {
            var moods= await _repository.GetAllAsync();
            if (moods is null)
            {
                return DarwinResponse<List<GetMoodResponse>>.Fail("NotFound.");
            }
            return DarwinResponse<List<GetMoodResponse>>.Success(moods.Adapt<List<GetMoodResponse>>());
        }
    }
}
