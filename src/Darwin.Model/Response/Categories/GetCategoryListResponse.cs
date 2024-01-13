namespace Darwin.Model.Response.Categories;

public class GetCategoryListResponse : PaginatedResponse
{
    public List<GetCategoryResponse> Items { get; set; }
}
