using Darwin.Domain.ResponseModels.Contents;

namespace Darwin.Domain.RepositoryCore;

public interface IContentRepository
{
    Task<GetContentByIdResponse> GetById(Guid id);
    Task<List<GetContentResponse>> GetAllAsync();
    Task<List<SearchContentResponse>> FullTextSearchAsync(string searchText);
    Task<List<SearchContentResponse>> SearchContentsByNameAsync(string searchText);
    Task<List<GetContentResponse>> GetListAsync(int offset, int pageSize);
    Task<int> GetTotalRowCountAsync();
}
