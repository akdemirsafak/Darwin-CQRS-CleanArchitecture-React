using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Categories;
using Darwin.Domain.ResponseModels.Categories;
using Darwin.Persistance.Mapping;

namespace Darwin.Persistance.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly ICategoryRepository _categoryReadRepository;
    CategoryMapper mapper = new CategoryMapper();
    public CategoryService(IGenericRepository<Category> categoryRepository,
        ICategoryRepository categoryReadRepository)
    {
        _categoryRepository = categoryRepository;
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<CreatedCategoryResponse> CreateAsync(CreateCategoryRequest request, string imageUrl)
    {
        var entity= mapper.CreateCategoryRequestToCategory(request);
        entity.ImageUrl = imageUrl;
        await _categoryRepository.CreateAsync(entity);

        return mapper.CategoryToCreatedCategoryResponse(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category=await _categoryRepository.GetAsync(x=>x.Id==id);
        category.IsUsable = false;
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task<List<GetCategoryResponse>> GetAllAsync()
    {
        return await _categoryReadRepository.GetAllAsync();
    }

    public async Task<GetCategoryResponse> GetByIdAsync(Guid id)
    {
        GetCategoryResponse category= await _categoryReadRepository.GetByIdAsync(id);
        return category;
    }

    public async Task<GetCategoryListResponse> GetListAsync(GetPaginationListRequest request)
    {
        //Dapper
        var offset= (request.Page-1)*request.PageSize;
        var totalRowCount= await _categoryReadRepository.GetTotalRowCountAsync();
        var datas= await _categoryReadRepository.GetListAsync(offset,request.PageSize);

        var totalPages = (int)Math.Ceiling(totalRowCount / (double)request.PageSize);
        var getMoodListResponse= new GetCategoryListResponse()
        {
            CurrentPage=request.Page,
            Items=datas,
            PageSize=request.PageSize,
            TotalCount=totalRowCount,
            TotalPages=totalPages,
        };
        return getMoodListResponse;

        //var queryable=_categoryRepository.GetList();
        //Paginate<Category> paginate= Paginate<Category>.ToPagedList(queryable,request.Page,request.PageSize);
        //return paginate.Adapt<GetCategoryListResponse>();
    }

    public async Task<UpdatedCategoryResponse> UpdateAsync(Guid id, UpdateCategoryRequest request)
    {
        var existCategory = await _categoryRepository.GetAsync(x => x.Id == id);
        //if (existCategory == null) //throw exception

        existCategory.ImageUrl = request.ImageUrl;
        existCategory.Name = request.Name;
        existCategory.IsUsable = request.IsUsable;

        await _categoryRepository.UpdateAsync(existCategory);
        return mapper.CategoryToUpdatedCategoryResponse(existCategory);
    }
}
