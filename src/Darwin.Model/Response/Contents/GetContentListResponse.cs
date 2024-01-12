namespace Darwin.Model.Response.Contents;

public class GetContentListResponse : PaginatedResponse
{
    public List<GetContentResponse> Items { get; set; }
}
