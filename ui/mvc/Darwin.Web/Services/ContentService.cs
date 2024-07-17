using Darwin.Shared.Dtos;
using Darwin.Web.Models.Contents;
using Darwin.Web.ViewModels;

namespace Darwin.Web.Services;

public class ContentService : IContentService
{
    private readonly HttpClient _httpClient;

    public ContentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(CreateContentDto createContentDto)
    {
        var response = await _httpClient.PostAsJsonAsync("content", createContentDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while creating the content");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<ContentViewModel>>();
        if (content.Errors is not null)
            throw new Exception("İçerik oluşturulurken bir hata oluştu.");
    }

    public async Task DeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"content/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while deleting the content");
        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<NoContentDto>>();
        if (content.Errors is not null)
            throw new Exception("Silme işlemi servis tarafında başarısız.");
    }

    public async Task<List<ContentViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("content");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while fetching the content");
        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<List<ContentViewModel>>>();
        if (content.Errors is not null)
            throw new Exception("İçerikler getirilirken bir hata oluştu.");
        return content.Data;
    }

    public async Task<ContentViewModel> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"content/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while fetching the content");
        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<ContentViewModel>>();
        if (content.Errors is not null)
            throw new Exception("İçerik getirilirken bir hata oluştu.");
        return content.Data;
    }

    public async Task UpdateAsync(UpdateContentDto updateContentDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"content/{updateContentDto.Id}", updateContentDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while updating the content");
        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<ContentViewModel>>();
        if (content.Errors is not null)
            throw new Exception("Güncelleme işlemi servis tarafında başarısız.");
    }
}
