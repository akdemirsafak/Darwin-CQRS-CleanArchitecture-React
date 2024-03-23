using Dapper;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.ResponseModels.Contents;
using Darwin.Domain.ResponseModels.PlayLists;
using Microsoft.Extensions.Configuration;

namespace Darwin.Persistance.Repository.DapperRepository;

public sealed class PlayListRepository : BaseRepository, IPlayListRepository
{
    public PlayListRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<List<GetPlayListResponse>> GetAllAsync()
    {

        var query = @"Select* from ""PlayLists"" Where ""IsPublic"" ";

        var playLists = await _dbConnection.QueryAsync<GetPlayListResponse>(query);

        return playLists.ToList();
    }

    public async Task<List<GetPlayListResponse>> GetAllListsOfUserAsync(string id)
    {
        var query = @"Select* from ""PlayLists"" where ""CreatorId""=@id";
        var playLists = await _dbConnection.QueryAsync<GetPlayListResponse>(query, new { id });
        return playLists.ToList();
    }

    public async Task<GetPlayListByIdResponse> GetByIdAsync(Guid id)
    {
        var query = @"Select * from ""PlayLists"" p
                FULL JOIN 
                    ""ContentPlayList"" cp ON p.""Id"" = cp.""PlayListsId""
                FULL JOIN 
                    ""Contents"" c ON c.""Id"" = cp.""ContentsId""
                        where p.""Id""=@id";

        var lookup = new Dictionary<Guid, GetPlayListByIdResponse>();

        var playList = await _dbConnection.QueryAsync<GetPlayListByIdResponse, GetContentResponse, GetPlayListByIdResponse>(
    query,
    (playlist, content) =>
    {
        if (!lookup.TryGetValue(playlist.Id, out var playlistEntry))
        {
            playlistEntry = playlist;
            playlistEntry.Contents = new List<GetContentResponse>();
            lookup.Add(playlistEntry.Id, playlistEntry);
        }

        if (content != null)
            playlistEntry.Contents.Add(content);

        return playlistEntry;
    },
    splitOn: "Id",
    param: new { id }
);

        return playList.Distinct().FirstOrDefault();
    }
}
