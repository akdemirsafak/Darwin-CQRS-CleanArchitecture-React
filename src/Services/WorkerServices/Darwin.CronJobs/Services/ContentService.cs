using Darwin.CronJobs.Models;
using Darwin.Shared.Dtos;

namespace Darwin.CronJobs.Services;

public class ContentService : IContentService
{
    private readonly HttpClient _httpClient;

    public ContentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<GetContentDto>> GetContentsAsync()
    {
        var content= await _httpClient.GetFromJsonAsync<DarwinResponse<List<GetContentDto>>>("content");
        return content.Data;
    }
}
