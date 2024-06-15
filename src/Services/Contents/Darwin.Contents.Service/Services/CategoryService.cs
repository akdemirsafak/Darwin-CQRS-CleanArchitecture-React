using AutoMapper;
using Darwin.Contents.Core.AbstractRepositories;
using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Category;
using Darwin.Contents.Core.Entities;
using Darwin.Contents.Core.RequestModels;
using Darwin.Contents.Core.RequestModels.Categories;
using Darwin.Contents.Service.Helper;
using Darwin.Shared.Auth;

namespace Darwin.Contents.Service.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public CategoryService(IGenericRepository<Category> categoryRepository,
        IMapper mapper,
        ICurrentUser currentUser)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<CreatedCategoryResponse> CreateAsync(CreateCategoryRequest request, string imageUrl)
    {
        var entity= _mapper.Map<Category>(request);
        entity.ImageUrl = imageUrl;
        await _categoryRepository.CreateAsync(entity);

        return _mapper.Map<CreatedCategoryResponse>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category=await _categoryRepository.GetAsync(x=>x.Id==id);
        await _categoryRepository.RemoveAsync(category);
    }

    public async Task<List<GetCategoryResponse>> GetAllAsync()
    {
        var categories= await _categoryRepository.GetAllAsync();
        return _mapper.Map<List<GetCategoryResponse>>(categories);
    }

    public async Task<GetCategoryResponse> GetByIdAsync(Guid id)
    {

        var category = await _categoryRepository.GetAsync(x => x.Id == id);

        return _mapper.Map<GetCategoryResponse>(category);
    }

    public async Task<GetCategoryListResponse> GetListAsync(GetPaginationListRequest request)
    {

        var queryable=await _categoryRepository.GetListAsync();
        Paginate<Category> paginate= Paginate<Category>.ToPagedList(queryable,request.Page,request.PageSize);
        return _mapper.Map<GetCategoryListResponse>(paginate);
    }

    public async Task<UpdatedCategoryResponse> UpdateAsync(Guid id, UpdateCategoryRequest request)
    {
        var existCategory = await _categoryRepository.GetAsync(x => x.Id == id);
        //if (existCategory == null) //throw exception

        existCategory.ImageUrl = request.ImageUrl;
        existCategory.Name = request.Name;

        await _categoryRepository.UpdateAsync(existCategory);

        return _mapper.Map<UpdatedCategoryResponse>(existCategory);
    }
}
