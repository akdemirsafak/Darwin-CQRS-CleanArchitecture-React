namespace Darwin.Contents.Core.Dtos.Responses.Mood;

public class GetMoodListResponse : PaginatedResponse
{
    public List<GetMoodResponse> Items { get; set; }
}
