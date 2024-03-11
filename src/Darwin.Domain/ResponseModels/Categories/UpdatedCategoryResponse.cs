namespace Darwin.Domain.ResponseModels.Categories;

public class UpdatedCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsUsable { get; set; }
}
