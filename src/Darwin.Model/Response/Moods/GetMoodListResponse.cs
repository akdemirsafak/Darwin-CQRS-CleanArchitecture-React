namespace Darwin.Model.Response.Moods;

public class GetMoodListResponse : PaginatedResponse
{
    public List<GetMoodResponse> Items { get; set; }
}
