using AutoMapper;
using Darwin.Contents.Core.Dtos.Responses.Mood;
using Darwin.Contents.Core.Entities;
using Darwin.Contents.Core.RequestModels.Moods;
using Darwin.Contents.Service.Helper;

namespace Darwin.Contents.Service.Mappers;

public class MoodMapper : Profile
{
    public MoodMapper()
    {
        CreateMap<Mood, GetMoodResponse>();
        CreateMap<CreateMoodRequest, Mood>();
        CreateMap<Mood, CreatedMoodResponse>();
        CreateMap<Mood, UpdatedMoodResponse>();
        CreateMap<Paginate<Mood>, GetMoodListResponse>();
    }
}
