using AutoMapper;
using Darwin.Contentlists.Core.Dtos;
using Darwin.Contentlists.Core.Entities;
using Darwin.Contentlists.Core.Repositories;
using Darwin.Contentlists.Core.Services;
using Darwin.Shared.Auth;
using Darwin.Shared.Dtos;

namespace Darwin.Contentlists.Service.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IMapper _mapper;


    public PlaylistService(
        IPlaylistRepository playlistRepository,
        IMapper mapper)
    {
        _playlistRepository = playlistRepository;
        _mapper = mapper;
    }

    public async Task<DarwinResponse<NoContentDto>> CreateAsync(CreatePlaylistRequest request)
    {
        var playlist= _mapper.Map<Playlist>(request);
        await _playlistRepository.CreateAsync(playlist);
        return DarwinResponse<NoContentDto>.Success(204);
    }

    public async Task<DarwinResponse<NoContentDto>> DeleteAsync(Guid id)
    {
        var playlist = await _playlistRepository.GetByIdAsync(id);
        if (playlist is null)
            return DarwinResponse<NoContentDto>.Fail("Playlist not found");

        await _playlistRepository.Delete(playlist);
        return DarwinResponse<NoContentDto>.Success(204);
    }

    public async Task<DarwinResponse<List<GetPlaylistResponse>>> GetAllAsync()
    {
        var playlists=await _playlistRepository.GetAllAsync();
        var response=_mapper.Map<List<GetPlaylistResponse>>(playlists);
        return DarwinResponse<List<GetPlaylistResponse>>.Success(response,200);
    }

    public async Task<DarwinResponse<GetPlaylistResponse>> GetByIdAsync(Guid id)
    {
        var playlist= await _playlistRepository.GetByIdAsync(id);

        return DarwinResponse<GetPlaylistResponse>.Success(_mapper.Map<GetPlaylistResponse>(playlist),200);
    }

    public async Task<DarwinResponse<NoContentDto>> UpdateAsync(Guid id, UpdatePlaylistRequest request)
    {
        var playlist= await _playlistRepository.GetByIdAsync(id);
        if (playlist is null)
            return DarwinResponse<NoContentDto>.Fail("Playlist not found");
        playlist.IsPublic = request.isPublic;
        playlist.Name = request.name;
        playlist.Description = request.description;
        playlist.ContentIds = request.contentIds;

        await _playlistRepository.Update(playlist);
        return DarwinResponse<NoContentDto>.Success(204);

    }
}
