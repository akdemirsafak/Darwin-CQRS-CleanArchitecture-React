using AutoMapper;
using Darwin.Core.Entities;
using Darwin.Model.Request.Musics;
using Darwin.Model.Response.Musics;

namespace Darwin.Model.Mappers;

public class MusicMapper : Profile
{
    public MusicMapper()
    {
        CreateMap<GetMusicResponse, Music>().ReverseMap();

        CreateMap<SearchMusicResponse, Music>().ReverseMap();

        CreateMap<CreateMusicRequest, Music>().ReverseMap();
    }
}
