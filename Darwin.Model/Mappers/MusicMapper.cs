using AutoMapper;
using Darwin.Core.Entities;
using Darwin.Model.Response.Musics;

namespace Darwin.Model.Mappers;

public class MusicMapper : Profile
{
    public MusicMapper()
    {
        CreateMap<Music, GetMusicResponse>()
            .ReverseMap();

        CreateMap<Music, SearchMusicResponse>()
            .ReverseMap();

        CreateMap<Music, CreatedMusicResponse>()
            .ReverseMap();

        CreateMap<Music, UpdatedMusicResponse>()
            .ReverseMap();
    }
}
