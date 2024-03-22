using Microsoft.AspNetCore.Http;

namespace Darwin.Domain.RequestModels.Categories;

public record CreateCategoryRequest(string Name,
    IFormFile ImageFile,
    bool IsUsable=true);
