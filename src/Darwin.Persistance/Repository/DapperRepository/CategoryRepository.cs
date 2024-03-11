using Dapper;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.ResponseModels.Categories;
using Microsoft.Extensions.Configuration;

namespace Darwin.Persistance.Repository.DapperRepository;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<GetCategoryResponse> GetByIdAsync(Guid id)
    {
        var query = $"Select * from \"Categories\" where \"Id\"=@id";
        var category= await _dbConnection.QuerySingleOrDefaultAsync<GetCategoryResponse>(query,new {id});
        return category;
    }
}
