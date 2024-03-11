using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Domain.RepositoryCore;
using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Moods;
using Darwin.Domain.ResponseModels.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Persistance.Helper;
using Mapster;

namespace Darwin.Persistance.Services;

public sealed class MoodService : IMoodService
{
    private readonly IGenericRepository<Mood> _moodRepository;

    public MoodService(IGenericRepository<Mood> moodRepository)
    {
        _moodRepository = moodRepository;
    }

    public async Task<CreatedMoodResponse> CreateAsync(string name, bool isUsable, string imageUrl = null)
    {
        Mood mood= new()
        {
            Name=name,
            ImageUrl=imageUrl,
            IsUsable=isUsable
        };
        var createdMood=await _moodRepository.CreateAsync(mood);
        return createdMood.Adapt<CreatedMoodResponse>();
    }

    public async Task<List<GetMoodResponse>> GetAllAsync()
    {
        var moods = await _moodRepository.GetAllAsync();
        return moods.Adapt<List<GetMoodResponse>>();
    }

    public async Task<GetMoodListResponse> GetListAsync(GetPaginationListRequest request)
    {
        var queryable=_moodRepository.GetList();
        Paginate<Mood> paginate= Paginate<Mood>.ToPagedList(queryable,request.Page,request.PageSize);
        return paginate.Adapt<GetMoodListResponse>();
    }

    public async Task<UpdatedMoodResponse> UpdateAsync(Guid id, UpdateMoodRequest request)
    {
        var mood = await _moodRepository.GetAsync(x => x.Id == id);

        mood.ImageUrl = request.ImageUrl;
        mood.Name = request.Name;
        mood.IsUsable = request.IsUsable;
        await _moodRepository.UpdateAsync(mood);
        return mood.Adapt<UpdatedMoodResponse>();
    }
}
