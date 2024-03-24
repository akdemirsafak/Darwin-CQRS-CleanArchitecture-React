using Darwin.Domain.Entities;
using Darwin.Domain.RequestModels.Moods;
using Darwin.Domain.ResponseModels.Moods;
using Darwin.Model.Response.Moods;
using Riok.Mapperly.Abstractions;

namespace Darwin.Persistance.Mapping;

[Mapper]
public partial class MoodMapper
{
    public partial CreatedMoodResponse MoodToCreatedMoodResponse(Mood mood);
    public partial UpdatedMoodResponse MoodToUpdatedMoodResponse(Mood mood);
    public partial Mood CreateMoodRequestToMoodResponse(CreateMoodRequest mood);
}
