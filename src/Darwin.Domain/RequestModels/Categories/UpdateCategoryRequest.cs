namespace Darwin.Domain.RequestModels.Categories;

public record UpdateCategoryRequest(string Name, string ImageUrl, bool IsUsable);
