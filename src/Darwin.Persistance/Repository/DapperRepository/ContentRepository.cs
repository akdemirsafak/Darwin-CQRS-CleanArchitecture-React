using Dapper;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.ResponseModels.Categories;
using Darwin.Domain.ResponseModels.Contents;
using Darwin.Domain.ResponseModels.Moods;
using Microsoft.Extensions.Configuration;


namespace Darwin.Persistance.Repository.DapperRepository;

public sealed class ContentRepository : BaseRepository, IContentRepository
{
    public ContentRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<List<SearchContentResponse>> FullTextSearchAsync(string searchText)
    {

        var query = @$"SELECT * FROM ""Contents"" WHERE to_tsvector('english', ""Name"") @@ to_tsquery('english', @searchText);";

        var response= await _dbConnection.QueryAsync<SearchContentResponse>(query, new { searchText });

        return response.ToList();
        //throw new NotImplementedException();
    }

    public async Task<List<GetContentResponse>> GetAllAsync()
    {
        var query= "Select * from \"Contents\"";
        var response= await _dbConnection.QueryAsync<GetContentResponse>(query);
        return response.ToList();
    }

    public async Task<GetContentByIdResponse> GetById(Guid id)
    {
        var query = @"
            SELECT 
                c.*,
                m.*,
                cat.*
            FROM 
                ""Contents"" c
            FULL JOIN 
                ""ContentMood"" cm ON c.""Id"" = cm.""ContentsId""
            FULL JOIN 
                ""Moods"" m ON m.""Id"" = cm.""MoodsId""
            FULL JOIN
                ""CategoryContent"" cc ON c.""Id"" = cc.""ContentsId""
            FULL JOIN
                ""Categories"" cat ON cat.""Id"" = cc.""CategoriesId""
            WHERE 
                c.""Id"" = @id;";


        var contents = await _dbConnection.QueryAsync<GetContentByIdResponse,GetMoodResponse,GetCategoryResponse,GetContentByIdResponse>(query,

            (content,mood,category)=>
            {
                content.Moods=new List<GetMoodResponse>();
                content.Categories=new List<GetCategoryResponse>();
                content.Moods.Add(mood);
                content.Categories.Add(category);
                return content;
            },
            splitOn:"Id",
            param:new { @id=id }
            );
        return contents.First();
    }
    public async Task<List<SearchContentResponse>> SearchContentsByNameAsync(string searchText)
    {
        //var query= @"Select * from ""Contents"" where ""Name"" ILIKE '%@searchText%'";

        var query= @"Select * from ""Contents"" where ""Name"" ILIKE '%' || @searchText || '%'";
        var contents=await _dbConnection.QueryAsync<SearchContentResponse>(query,new {searchText});
        return contents.ToList();
    }
    public async Task<List<GetContentResponse>> GetListAsync(int offset, int pageSize)
    {

        string query=@"SELECT *
            FROM ""Contents"" ORDER BY ""CreatedOnUtc""
            OFFSET @Offset LIMIT @PageSize";
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("Offset", offset);
        dynamicParameters.Add("@PageSize", pageSize);
        var response = await _dbConnection.QueryAsync<GetContentResponse>(query, dynamicParameters);
        return response.ToList();
    }
    public async Task<int> GetTotalRowCountAsync()
    {
        string query=@"SELECT Count(*) FROM ""Contents"" ";
        int totalRowCount = await _dbConnection.ExecuteScalarAsync<int>(query);
        return totalRowCount;
    }


}
