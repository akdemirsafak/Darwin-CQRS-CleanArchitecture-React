namespace Darwin.Domain.ResponseModels.Categories;
public class GetCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsUsable { get; set; }
}
