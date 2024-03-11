using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.RequestModels.PlayLists;
using Darwin.Domain.ResponseModels.PlayLists;
using Mapster;

namespace Darwin.Persistance.Services;

public sealed class PlayListService : IPlayListService
{
    private readonly IGenericRepository<PlayList> _playListRepository;
    private readonly IGenericRepository<Content> _contentRepository;

    public PlayListService(IGenericRepository<PlayList> playListRepository, IGenericRepository<Content> contentRepository)
    {
        _playListRepository = playListRepository;
        _contentRepository = contentRepository;
    }

    public async Task<GetPlayListByIdResponse> AddContentsToPlayList(AddContentsToPlayListRequest request)
    {
        PlayList hasPlayList = await _playListRepository.GetAsync(x=>x.Id == request.playListId);
        if (hasPlayList is null)
            throw new Exception("PlayList bulunamadı.");

        foreach (var contentId in request.contentIds)
        {
            var content = await _contentRepository.GetAsync(x => x.Id == contentId);
            if (content is null)
                throw new Exception("Bazı içerikler bulunamadı.");
            else
            {
                hasPlayList.Contents.Add(content);
            }
        }

        await _playListRepository.UpdateAsync(hasPlayList);
        return hasPlayList.Adapt<GetPlayListByIdResponse>();
    }

    public async Task<CreatedPlayListResponse> CreateAsync(CreatePlayListRequest request, string creatorId)
    {
        var entity= request.Adapt<PlayList>();
        entity.CreatorId = creatorId;
        await _playListRepository.CreateAsync(entity);

        return entity.Adapt<CreatedPlayListResponse>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var hasPlayList= await _playListRepository.GetAsync(x=>x.Id==id);

        if (hasPlayList is null)
            throw new Exception("Playlist bulunamadı.");

        if (hasPlayList.IsFavorite)
            throw new Exception("Favori içeriklerim listesi silinemez.");

        hasPlayList.IsUsable = false;
        await _playListRepository.UpdateAsync(hasPlayList);
    }

    public async Task<List<GetPlayListResponse>> GetAllAsync()
    {
        var myLists= await _playListRepository.GetAllAsync();
        return myLists.Adapt<List<GetPlayListResponse>>();
    }

    public async Task<List<GetPlayListResponse>> GetAllListsOfUser(string currentUserId)
    {
        var myLists= await _playListRepository.GetAllAsync(x=>x.CreatorId==currentUserId);
        return myLists.Adapt<List<GetPlayListResponse>>();
    }

    public async Task<GetPlayListByIdResponse> GetByIdAsync(Guid id)
    {
        var playList = await _playListRepository.GetAsync(x => x.Id == id);
        if (playList is null)
            throw new Exception("Çalma listesi bulunamadı.");

        return playList.Adapt<GetPlayListByIdResponse>();
    }

    public Task<GetPlayListResponse> GetMyPlayListsByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<GetPlayListByIdResponse> RemoveContentsFromPlayList(RemoveContentsFromPlayListRequest request)
    {
        var existPlayList = await _playListRepository.GetAsync(x=>x.Id==request.playListId);
        if (existPlayList is null)
            throw new Exception("PlayList bulunamadı.");

        foreach (var requestContentId in request.contentIds)
        {
            var content = await _contentRepository.GetAsync(x => x.Id == requestContentId);
            existPlayList.Contents.Remove(content);
        }
        return existPlayList.Adapt<GetPlayListByIdResponse>();
    }

    public async Task<UpdatedPlayListResponse> UpdateAsync(Guid id, UpdatePlayListRequest request, string creatorId)
    {
        var hasList= await _playListRepository.GetAsync(x=>x.Id==id && x.Creator.Id==creatorId);
        if (hasList is null)
            throw new Exception("Liste bulunamadı.");

        if (!hasList.IsFavorite)
            hasList.IsUsable = request.IsUsable;

        hasList.Name = request.Name;
        hasList.Description = request.Description;
        hasList.IsPublic = request.IsPublic;

        await _playListRepository.UpdateAsync(hasList);
        return hasList.Adapt<UpdatedPlayListResponse>();
    }
}
