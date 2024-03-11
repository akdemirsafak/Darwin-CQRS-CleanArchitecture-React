using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Contents;
using Darwin.Domain.ResponseModels.Contents;
using Darwin.Persistance.Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Persistance.Services;

public sealed class ContentService : IContentService
{
    private readonly IGenericRepository<Content> _contentRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<Mood> _moodRepository;

    public ContentService(IGenericRepository<Content> contentRepository,
        IGenericRepository<Category> categoryRepository,
        IGenericRepository<Mood> moodRepository)
    {
        _contentRepository = contentRepository;
        _categoryRepository = categoryRepository;
        _moodRepository = moodRepository;
    }

    public async Task<CreatedContentResponse> CreateAsync(CreateContentRequest request)
    {
        List<Mood> moodList=new();
        foreach (var moodId in request.MoodIds)
        {
            Mood existMood = await _moodRepository.GetAsync(x => x.Id == moodId);
            if (existMood is not null)
            {
                moodList.Add(existMood);
            }
        }

        //Categories
        List<Category> categoryList=new();
        foreach (var categoryId in request.CategoryIds)
        {
            Category existCategory = await _categoryRepository.GetAsync(x => x.Id == categoryId);
            if (existCategory != null)
            {
                categoryList.Add(existCategory);
            }
        }

        var content = new Content()
        {
            Name = request.Name,
            Lyrics= request.Lyrics,
            ImageUrl = request.ImageUrl,
            IsUsable = request.IsUsable,
            Categories =categoryList,
            Moods =moodList
        };

        //SaveOperations
        await _contentRepository.CreateAsync(content);
        return content.Adapt<CreatedContentResponse>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var content = await _contentRepository.GetAsync(x => x.Id == id);

        content.IsUsable = false;

        await _contentRepository.UpdateAsync(content);
    }

    public async Task<List<GetContentResponse>> GetAllAsync()
    {
        var contents= await _contentRepository.GetAllAsync();
        return contents.Adapt<List<GetContentResponse>>();
    }

    public async Task<GetContentByIdResponse> GetByIdAsync(Guid id)
    {
        var content= await _contentRepository.GetAsync(x=>x.Id == id);
        return content.Adapt<GetContentByIdResponse>();
    }

    public async Task<GetContentListResponse> GetListAsync(GetPaginationListRequest request)
    {
        var queryable=_contentRepository.GetList();
        Paginate<Content> paginate= Paginate<Content>.ToPagedList(queryable,request.Page,request.PageSize);
        return paginate.Adapt<GetContentListResponse>();
    }

    public async Task<List<SearchContentResponse>> SearchAsync(string searchText)
    {
        var contents = await _contentRepository.GetAllAsync(x =>
            x.Name.Contains(searchText));
        return contents.Adapt<List<SearchContentResponse>>();
    }

    public async Task<UpdatedContentResponse> UpdateAsync(Guid id, UpdateContentRequest request)
    {
        var content = await _contentRepository.GetAsync(x => x.Id == id);

        content.ImageUrl = request.ImageUrl;
        content.Name = request.Name != content.Name ? request.Name : content.Name;
        content.Lyrics = request.Lyrics != content.Lyrics ? request.Lyrics : content.Lyrics;
        content.IsUsable = request.IsUsable;

        await _contentRepository.UpdateAsync(content);
        return content.Adapt<UpdatedContentResponse>();
    }

    public async Task<List<GetContentResponse>> GetNewContentsAsync(int size)
    {
        var queryable = _contentRepository.GetList();
        var contents=await queryable.OrderByDescending(x=>x.CreatedOnUtc).Take(size).ToListAsync();

        return contents.Adapt<List<GetContentResponse>>();
    }
}
