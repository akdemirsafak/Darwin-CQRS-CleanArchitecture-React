namespace Darwin.Domain.ResponseModels.Contents;

public class GetContentListResponse : PaginatedResponse
{
    public List<GetContentResponse> Items { get; set; }
}
