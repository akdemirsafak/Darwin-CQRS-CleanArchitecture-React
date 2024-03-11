using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Categories;
using Darwin.Domain.ResponseModels.Categories;
using Darwin.Persistance.Helper;
using Mapster;

namespace Darwin.Persistance.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly ICategoryRepository _categoryReadRepository;

    public CategoryService(IGenericRepository<Category> categoryRepository,
        ICategoryRepository categoryReadRepository)
    {
        _categoryRepository = categoryRepository;
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<CreatedCategoryResponse> CreateAsync(CreateCategoryRequest request, string imageUrl)
    {
        var entity = request.Adapt<Category>();
        entity.ImageUrl = imageUrl;
        await _categoryRepository.CreateAsync(entity);
        return entity.Adapt<CreatedCategoryResponse>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var category=await _categoryRepository.GetAsync(x=>x.Id==id);
        category.IsUsable = false;
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task<List<GetCategoryResponse>> GetAllAsync()
    {
        var categories= await _categoryRepository.GetAllAsync();
        return categories.Adapt<List<GetCategoryResponse>>();
    }

    public async Task<GetCategoryResponse> GetByIdAsync(Guid id)
    {
        GetCategoryResponse category= await _categoryReadRepository.GetByIdAsync(id);
        return category;
    }

    public async Task<GetCategoryListResponse> GetListAsync(GetPaginationListRequest request)
    {
        var queryable=_categoryRepository.GetList();
        Paginate<Category> paginate= Paginate<Category>.ToPagedList(queryable,request.Page,request.PageSize);
        return paginate.Adapt<GetCategoryListResponse>();
    }

    public async Task<UpdatedCategoryResponse> UpdateAsync(Guid id, UpdateCategoryRequest request)
    {
        var existCategory = await _categoryRepository.GetAsync(x => x.Id == id);
        //if (existCategory == null) //throw exception

        existCategory.ImageUrl = request.ImageUrl;
        existCategory.Name = request.Name;
        existCategory.IsUsable = request.IsUsable;

        await _categoryRepository.UpdateAsync(existCategory);
        return existCategory.Adapt<UpdatedCategoryResponse>();
    }
}
