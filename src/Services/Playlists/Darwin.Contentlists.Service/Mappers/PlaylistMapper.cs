using AutoMapper;
using Darwin.Contentlists.Core.Dtos;
using Darwin.Contentlists.Core.Entities;

namespace Darwin.Contentlists.Service.Mappers;

public sealed class PlaylistMapper : Profile
{
    public PlaylistMapper()
    {
        CreateMap<CreatePlaylistRequest, Playlist>();
        CreateMap<Playlist, GetPlaylistResponse>();
    }
}
