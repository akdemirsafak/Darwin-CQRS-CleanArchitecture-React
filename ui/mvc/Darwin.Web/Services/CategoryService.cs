using Darwin.Shared.Dtos;
using Darwin.Web.Models.Categories;
using Darwin.Web.ViewModels;

namespace Darwin.Web.Services;

public class CategoryService : ICategoryService
{

    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(CreateCategoryDto createCategoryDto)
    {
        var response = await _httpClient.PostAsJsonAsync("category", createCategoryDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while creating the category");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<CategoryViewModel>>();
        if (content.Errors is not null)
            throw new Exception("Kategori oluşturulurken bir hata oluştu.");
    }

    public async Task DeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"category/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while deleting the category");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<NoContentDto>>();
        if (content.Errors is not null)
            throw new Exception("Silme işlemi servis tarafında başarısız.");
    }

    public async Task<List<CategoryViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("category");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while fetching the category");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<List<CategoryViewModel>>>();
        if (content.Errors is not null)
            throw new Exception("Kategoriler getirilirken bir hata oluştu.");

        return content.Data;
    }

    public async Task<CategoryViewModel> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/category/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while fetching the category");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<CategoryViewModel>>();
        if (content.Errors is not null)
            throw new Exception("Kategori getirilirken bir hata oluştu.");
        return content.Data;
    }

    public async Task UpdateAsync(UpdateCategoryDto updateCategoryDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/category/{updateCategoryDto.Id}", updateCategoryDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong while updating the category");

        var content = await response.Content.ReadFromJsonAsync<DarwinResponse<CategoryViewModel>>();
        if (content.Errors is not null)
            throw new Exception("Kategori güncellenirken bir hata oluştu.");

    }
}
