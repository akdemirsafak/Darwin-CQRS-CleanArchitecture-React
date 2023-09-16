using AutoMapper;
using Darwin.Core.Entities;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Model.Response.Musics;

namespace Darwin.Model.Mappers;

public class MoodMapper : Profile
{
    public MoodMapper()
    {
        CreateMap<Mood, GetMoodResponse>()
            .ReverseMap();
        CreateMap<CreateMoodRequest, Mood>()
            .ReverseMap();
        CreateMap<CreatedMoodResponse, Mood>()
            .ReverseMap();
        CreateMap<UpdatedMoodResponse, Mood>()
            .ReverseMap();
    }
}
