namespace Darwin.Domain.ResponseModels.Moods;

public class GetMoodListResponse : PaginatedResponse
{
    public List<GetMoodResponse> Items { get; set; }
}
