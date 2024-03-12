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
    public async Task<List<GetCategoryResponse>> GetAllAsync()
    {
        var query= @"Select * from ""Categories""";
        var categories= await _dbConnection.QueryAsync<GetCategoryResponse>(query);
        return categories.ToList();
    }

    public async Task<List<GetCategoryResponse>> GetListAsync(int offset, int pageSize)
    {
        string query=@"SELECT *
            FROM ""Categories"" ORDER BY ""CreatedOnUtc""
            OFFSET @Offset LIMIT @PageSize";
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("Offset", offset);
        dynamicParameters.Add("@PageSize", pageSize);
        var response = await _dbConnection.QueryAsync<GetCategoryResponse>(query, dynamicParameters);
        return response.ToList();
    }

    public async Task<int> GetTotalRowCountAsync()
    {
        string query=@"SELECT Count(*) FROM ""Categories"" ";
        int totalRowCount = await _dbConnection.ExecuteScalarAsync<int>(query);
        return totalRowCount;
    }
}
