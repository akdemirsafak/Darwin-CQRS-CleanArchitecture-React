using Darwin.Domain.ResponseModels.Moods;

namespace Darwin.Domain.RepositoryCore;

public interface IMoodRepository
{
    Task<List<GetMoodResponse>> GetAllAsync();
    Task<List<GetMoodResponse>> GetAsync(int offset,int pageSize);
    Task<int> GetTotalRowCountAsync();
}
