namespace Darwin.Contents.Core.Dtos.Responses.Category;

public class GetCategoryListResponse : PaginatedResponse
{
    public List<GetCategoryResponse> Items { get; set; }
}
