namespace Darwin.Domain.ResponseModels.Categories;

public class CreatedCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsUsable { get; set; }
}
