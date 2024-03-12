using Darwin.Domain.Entities;
using Darwin.Domain.RequestModels.Categories;
using Darwin.Domain.ResponseModels.Categories;
using Riok.Mapperly.Abstractions;

namespace Darwin.Persistance.Mapping;

[Mapper]
public partial class CategoryMapper
{
    public partial CreatedCategoryResponse CategoryToCreatedCategoryResponse(Category category);
    public partial UpdatedCategoryResponse CategoryToUpdatedCategoryResponse(Category category);
    public partial Category CreateCategoryRequestToCategory(CreateCategoryRequest createCategoryRequest);
}