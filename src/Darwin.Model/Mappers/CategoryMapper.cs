using AutoMapper;
using Darwin.Core.Entities;
using Darwin.Model.Request.Categories;
using Darwin.Model.Response.Categories;

namespace Darwin.Model.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<GetCategoryResponse, Category>()
            .ReverseMap();
        CreateMap<CreateCategoryRequest, Category>()
            .ReverseMap();
        CreateMap<CreatedCategoryResponse, Category>()
            .ReverseMap();
        CreateMap<UpdatedCategoryResponse, Category>()
            .ReverseMap();
    }
}
