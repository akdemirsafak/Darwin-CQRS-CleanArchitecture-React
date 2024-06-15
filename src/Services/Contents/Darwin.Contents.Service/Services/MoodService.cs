using AutoMapper;
using Darwin.Contents.Core.AbstractRepositories;
using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Mood;
using Darwin.Contents.Core.Entities;
using Darwin.Contents.Core.RequestModels;
using Darwin.Contents.Core.RequestModels.Moods;
using Darwin.Contents.Service.Helper;

namespace Darwin.Contents.Service.Services;

public sealed class MoodService : IMoodService
{
    private readonly IGenericRepository<Mood> _moodRepository;
    private readonly IMapper _mapper;
    public MoodService(IGenericRepository<Mood> moodRepository, IMapper mapper)
    {
        _moodRepository = moodRepository;
        _mapper = mapper;
    }

    public async Task<CreatedMoodResponse> CreateAsync(string name, string imageUrl = null)
    {
        Mood mood= new()
        {
            Name=name,
            ImageUrl=imageUrl
        };
        var createdMood=await _moodRepository.CreateAsync(mood);
        return _mapper.Map<CreatedMoodResponse>(createdMood);
    }

    public async Task<List<GetMoodResponse>> GetAllAsync()
    {
        var moods= await _moodRepository.GetListAsync();
        return _mapper.Map<List<GetMoodResponse>>(moods);
    }

    public async Task<GetMoodListResponse> GetListAsync(GetPaginationListRequest request)
    {
        // EFCORE
        var queryable=await _moodRepository.GetListAsync();
        Paginate<Mood> paginate= Paginate<Mood>.ToPagedList(queryable,request.Page,request.PageSize);
        return _mapper.Map<GetMoodListResponse>(paginate);
    }

    public async Task<UpdatedMoodResponse> UpdateAsync(Guid id, UpdateMoodRequest request)
    {
        var mood = await _moodRepository.GetAsync(x => x.Id == id);
        mood.ImageUrl = request.ImageUrl;
        mood.Name = request.Name;
        await _moodRepository.UpdateAsync(mood);

        return _mapper.Map<UpdatedMoodResponse>(mood);
    }
}
