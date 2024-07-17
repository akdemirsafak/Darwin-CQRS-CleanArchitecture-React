using Darwin.Shared.Dtos;
using Darwin.Web.Models.Moods;
using Darwin.Web.ViewModels;

namespace Darwin.Web.Services;

public class MoodService : IMoodService
{
    private readonly HttpClient _httpClient;
    public MoodService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task CreateAsync(CreateMoodDto createMoodDto)
    {
        var response = await _httpClient.PostAsJsonAsync("mood", createMoodDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while creating the mood");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<MoodViewModel>>();
        if (content.Errors is not null)
            throw new Exception("Mood oluşturulurken bir hata oluştu.");

    }

    public async Task DeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"mood/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while deleting the mood");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<NoContentDto>>();

        if (content.Errors is not null)
            throw new Exception("Silme işlemi servis tarafında başarısız.");
    }

    public async Task<List<MoodViewModel>> GetAllAsync()
    {
        var response= await _httpClient.GetAsync("mood");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while fetching the mood");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<List<MoodViewModel>>>();
        if (content.Errors is not null)
            throw new Exception("Mood getirilirken bir hata oluştu.");

        return content.Data;
    }

    //public async Task<MoodViewModel> GetByIdAsync(Guid id)
    //{
    //    var response = await _httpClient.GetAsync($"mood/{id}");
    //    if (!response.IsSuccessStatusCode)
    //        throw new Exception("Something went wrong while fetching the mood");

    //    var content = await response.Content.ReadFromJsonAsync<DarwinResponse<MoodViewModel>>();
    //    if (content.Errors is not null)
    //        throw new Exception("Mood getirilirken bir hata oluştu.");

    //    return content.Data;
    //}

    public async Task UpdateAsync(UpdateMoodDto updateMoodDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"mood/{updateMoodDto.Id}", updateMoodDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while updating the mood");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<MoodViewModel>>();
        if (content.Errors is not null)
            throw new Exception("Güncelleme işlemi servis tarafında başarısız.");
    }
}
