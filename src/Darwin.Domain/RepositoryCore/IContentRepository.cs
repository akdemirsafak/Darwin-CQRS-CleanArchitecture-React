using Darwin.Domain.ResponseModels.Contents;

namespace Darwin.Domain.RepositoryCore;

public interface IContentRepository
{
    Task<GetContentByIdResponse> GetById(Guid id);
    Task<List<GetContentResponse>> GetAllAsync();
    Task<List<SearchContentResponse>> FullTextSearchAsync(string searchText);
}
