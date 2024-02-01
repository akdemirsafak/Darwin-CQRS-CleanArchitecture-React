using Microsoft.AspNetCore.Http;

namespace Darwin.Model.Request.Categories;

public record CreateCategoryRequest(string Name,
    IFormFile ImageFile,
    bool IsUsable);
