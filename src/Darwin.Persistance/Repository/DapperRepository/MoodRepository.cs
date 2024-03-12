using Dapper;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.ResponseModels.Moods;
using Microsoft.Extensions.Configuration;

namespace Darwin.Persistance.Repository.DapperRepository;

public sealed class MoodRepository : BaseRepository, IMoodRepository
{
    public MoodRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<List<GetMoodResponse>> GetAllAsync()
    {
        string query=@"Select * from ""Moods""";
        var moods= await _dbConnection.QueryAsync<GetMoodResponse>(query);
        return moods.ToList();
    }

    public async Task<List<GetMoodResponse>> GetListAsync(int offset, int pageSize)
    {
        string query=@"SELECT *
            FROM ""Moods"" ORDER BY ""CreatedOnUtc""
            OFFSET @Offset LIMIT @PageSize";
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("Offset", offset);
        dynamicParameters.Add("@PageSize", pageSize);
        var response = await _dbConnection.QueryAsync<GetMoodResponse>(query, dynamicParameters);
        return response.ToList();
    }
    public async Task<int> GetTotalRowCountAsync()
    {
        string query=@"SELECT Count(*) FROM ""Moods"" ";
        int totalRowCount = await _dbConnection.ExecuteScalarAsync<int>(query);
        return totalRowCount;
    }
}
