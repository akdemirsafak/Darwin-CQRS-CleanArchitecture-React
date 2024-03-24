namespace Darwin.Domain.ResponseModels.Categories;

public class GetCategoryListResponse : PaginatedResponse
{
    public List<GetCategoryResponse> Items { get; set; }
}
