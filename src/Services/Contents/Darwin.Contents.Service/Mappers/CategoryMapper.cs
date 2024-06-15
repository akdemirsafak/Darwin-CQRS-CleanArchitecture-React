using AutoMapper;
using Darwin.Contents.Core.Dtos.Responses.Category;
using Darwin.Contents.Core.Entities;
using Darwin.Contents.Core.RequestModels.Categories;
using Darwin.Contents.Service.Helper;

namespace Darwin.Contents.Service.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Category, GetCategoryResponse>();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CreatedCategoryResponse>();
        CreateMap<Category, UpdatedCategoryResponse>();
        CreateMap<Paginate<Category>, GetCategoryListResponse>();
    }
}
