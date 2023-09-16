namespace Darwin.Model.Request.Categories;

public record CreateCategoryRequest(string Name, string ImageUrl, bool IsUsable);
