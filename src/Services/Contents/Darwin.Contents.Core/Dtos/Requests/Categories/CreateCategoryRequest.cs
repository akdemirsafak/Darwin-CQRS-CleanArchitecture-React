using Microsoft.AspNetCore.Http;

namespace Darwin.Contents.Core.RequestModels.Categories;

public record CreateCategoryRequest(string Name,
    IFormFile ImageFile);
