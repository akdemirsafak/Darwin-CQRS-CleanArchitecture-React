using AutoMapper;
using Darwin.Contents.Core.AbstractRepositories;
using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Content;
using Darwin.Contents.Core.Entities;
using Darwin.Contents.Core.RequestModels;
using Darwin.Contents.Core.RequestModels.Contents;
using Darwin.Contents.Service.Helper;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Contents.Service.Services;

public sealed class ContentService : IContentService
{
    private readonly IGenericRepository<Content> _contentRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<Mood> _moodRepository;
    private readonly IMapper _mapper;

    public ContentService(IGenericRepository<Content> contentRepository,
        IGenericRepository<Category> categoryRepository,
        IGenericRepository<Mood> moodRepository,
        IMapper mapper)
    {
        _contentRepository = contentRepository;
        _categoryRepository = categoryRepository;
        _moodRepository = moodRepository;
        _mapper = mapper;
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
            Categories =categoryList,
            Moods =moodList
        };

        //SaveOperations
        await _contentRepository.CreateAsync(content);


        return _mapper.Map<CreatedContentResponse>(content);
    }

    public async Task DeleteAsync(Guid id)
    {
        var content = await _contentRepository.GetAsync(x => x.Id == id);

        await _contentRepository.RemoveAsync(content);

    }

    public async Task<List<GetContentResponse>> GetAllAsync()
    {
        var contents = await _contentRepository.GetAllAsync();

        return _mapper.Map<List<GetContentResponse>>(contents);
    }

    public async Task<GetContentByIdResponse> GetByIdAsync(Guid id)
    {
        var content = await _contentRepository.GetAsync(x => x.Id == id);
        return _mapper.Map<GetContentByIdResponse>(content);
    }

    public async Task<GetContentListResponse> GetListAsync(GetPaginationListRequest request)
    {
        //EF CORE
        var queryable=await _contentRepository.GetListAsync();
        Paginate<Content> paginate= Paginate<Content>.ToPagedList(queryable,request.Page,request.PageSize);
        return _mapper.Map<GetContentListResponse>(paginate);

    }

    public async Task<List<SearchContentResponse>> SearchAsync(string searchText)
    {
        //EF CORE
        var contents = await _contentRepository.GetAllAsync(x =>
            x.Name.ToLower().Contains(searchText.ToLower()));
        return _mapper.Map<List<SearchContentResponse>>(contents);
    }
    //public async Task<List<SearchContentResponse>> FullTextSearchAsync(string searchText)
    //{

    //    var contents = await _contentRepository.GetListAsync(x =>
    //        EF.Functions.(x.Name, searchText));
    //    return _mapper.Map<List<SearchContentResponse>>(contents);


    //    //var contents = await _contentReadRepository.FullTextSearchAsync(searchText);
    //    //return contents;
    //}

    public async Task<UpdatedContentResponse> UpdateAsync(Guid id, UpdateContentRequest request)
    {
        var content = await _contentRepository.GetAsync(x => x.Id == id);

        content.ImageUrl = request.ImageUrl;
        content.Name = request.Name != content.Name ? request.Name : content.Name;
        content.Lyrics = request.Lyrics != content.Lyrics ? request.Lyrics : content.Lyrics;

        await _contentRepository.UpdateAsync(content);
        return _mapper.Map<UpdatedContentResponse>(content);

    }

    public async Task<List<GetContentResponse>> GetNewContentsAsync(int size)
    {
        var queryable =await _contentRepository.GetListAsync();
        var contents=await queryable.OrderByDescending(x=>x.CreatedAt).Take(size).ToListAsync();

        return _mapper.Map<List<GetContentResponse>>(contents);
    }
}
