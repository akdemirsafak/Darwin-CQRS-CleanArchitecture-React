namespace Darwin.Contents.Core.Dtos.Responses.Content;

public class GetContentListResponse : PaginatedResponse
{
    public List<GetContentResponse> Items { get; set; }
}
