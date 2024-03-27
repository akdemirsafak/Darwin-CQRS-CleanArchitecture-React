using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Contents;
using Darwin.Domain.ResponseModels.Contents;
using Darwin.Persistance.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Persistance.Services;

public sealed class ContentService : IContentService
{
    private readonly IGenericRepository<Content> _contentRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<Mood> _moodRepository;

    private readonly IContentRepository _contentReadRepository;
    ContentMapper mapper= new ContentMapper();

    public ContentService(IGenericRepository<Content> contentRepository,
        IGenericRepository<Category> categoryRepository,
        IGenericRepository<Mood> moodRepository,
        IContentRepository contentReadRepository)
    {
        _contentRepository = contentRepository;
        _categoryRepository = categoryRepository;
        _moodRepository = moodRepository;
        _contentReadRepository = contentReadRepository;
    }

    public async Task<CreatedContentResponse> CreateAsync(CreateContentRequest request, string imageUrl)
    {
        List<Mood> moodList=new();
        foreach (var moodId in request.SelectedMoods)
        {
            Mood existMood = await _moodRepository.GetAsync(x => x.Id == moodId);
            if (existMood is not null)
            {
                moodList.Add(existMood);
            }
        }

        //Categories
        List<Category> categoryList=new();
        foreach (var categoryId in request.SelectedCategories)
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
            ImageUrl = imageUrl,
            IsUsable = request.IsUsable,
            Categories =categoryList,
            Moods =moodList
        };

        //SaveOperations
        await _contentRepository.CreateAsync(content);
        //return content.Adapt<CreatedContentResponse>();
        return mapper.ContentToCreatedContentResponse(content);
    }

    public async Task DeleteAsync(Guid id)
    {
        var content = await _contentRepository.GetAsync(x => x.Id == id);

       await _contentRepository.RemoveAsync(content);
    }

    public async Task<List<GetContentResponse>> GetAllAsync()
    {
        return await _contentReadRepository.GetAllAsync();
    }

    public async Task<GetContentByIdResponse> GetByIdAsync(Guid id)
    {
        return await _contentReadRepository.GetById(id);
    }

    public async Task<GetContentListResponse> GetListAsync(GetPaginationListRequest request)
    {
        //EF CORE
        //var queryable=_contentRepository.GetList();
        //Paginate<Content> paginate= Paginate<Content>.ToPagedList(queryable,request.Page,request.PageSize);
        //return paginate.Adapt<GetContentListResponse>();


        //Dapper
        var offset= (request.Page-1)*request.PageSize;
        var totalRowCount= await _contentReadRepository.GetTotalRowCountAsync();
        var datas= await _contentReadRepository.GetListAsync(offset,request.PageSize);

        var totalPages = (int)Math.Ceiling(totalRowCount / (double)request.PageSize);
        var getContentListResponse= new GetContentListResponse()
        {
            CurrentPage=request.Page,
            Items=datas,
            PageSize=request.PageSize,
            TotalCount=totalRowCount,
            TotalPages=totalPages,
        };
        return getContentListResponse;



    }

    public async Task<List<SearchContentResponse>> SearchAsync(string searchText)
    {
        //EF CORE
        //var contents = await _contentRepository.GetAllAsync(x =>
        //    x.Name.ToLower().Contains(searchText.ToLower()));
        //return contents.Adapt<List<SearchContentResponse>>();

        //DAPPER
        return await _contentReadRepository.SearchContentsByNameAsync(searchText);
    }
    public async Task<List<SearchContentResponse>> FullTextSearchAsync(string searchText)
    {
        var contents = await _contentReadRepository.FullTextSearchAsync(searchText);
        return contents;
    }

    public async Task<UpdatedContentResponse> UpdateAsync(Guid id, UpdateContentRequest request)
    {
        var content = await _contentRepository.GetAsync(x => x.Id == id);

        content.ImageUrl = request.ImageUrl;
        content.Name = request.Name != content.Name ? request.Name : content.Name;
        content.Lyrics = request.Lyrics != content.Lyrics ? request.Lyrics : content.Lyrics;
        content.IsUsable = request.IsUsable;

        await _contentRepository.UpdateAsync(content);
        return mapper.ContentToUpdatedContentResponse(content);
    }

    public async Task<List<GetContentResponse>> GetNewContentsAsync(int size)
    {
        var queryable = _contentRepository.GetList();
        var contents=await queryable.OrderByDescending(x=>x.CreatedOnUtc).Take(size).ToListAsync();

        return mapper.GetContentResponseListToContentList(contents);
    }
}
