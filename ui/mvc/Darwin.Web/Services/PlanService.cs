using Darwin.Shared.Dtos;
using Darwin.Web.Models.Plans;
using Darwin.Web.ViewModels;

namespace Darwin.Web.Services;

public class PlanService : IPlanService
{
    private readonly HttpClient _httpClient;

    public PlanService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(CreatePlanDto createPlanDto)
    {
        var response = await _httpClient.PostAsJsonAsync("plan", createPlanDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to create plan");

        var content= await response.Content.ReadFromJsonAsync<DarwinResponse<PlanViewModel>>();

        if (content.Errors is not null)
            throw new Exception(content.Errors.ToString());
    }

    public async Task DeleteAsync(Guid id)
    {
        var response= await _httpClient.DeleteAsync($"plan/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to delete plan");

        var content= await response.Content.ReadFromJsonAsync<DarwinResponse<bool>>();
        if (content.Errors is not null)
            throw new Exception(content.Errors.ToString());
    }

    public async Task<List<PlanViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("plan");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to get plans");

        var content= await response.Content.ReadFromJsonAsync<DarwinResponse<List<PlanViewModel>>>();
        if (content.Errors is not null)
            throw new Exception(content.Errors.ToString());

        return content.Data;

    }

    public async Task<PlanViewModel> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetFromJsonAsync<DarwinResponse<PlanViewModel>>($"plan/{id}");
        if (response.Errors is not null)
            throw new Exception(response.Errors.ToString());
        return response.Data;
    }

    public async Task UpdateAsync(UpdatePlanDto updatePlanDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"plan/{updatePlanDto.Id}", updatePlanDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to update plan");

        var content= await response.Content.ReadFromJsonAsync<DarwinResponse<PlanViewModel>>();
        if (content.Errors is not null)
            throw new Exception(content.Errors.ToString());

    }
}
