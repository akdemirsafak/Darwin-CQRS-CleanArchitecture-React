using Darwin.CronJobs.Models;
using Darwin.Shared.Dtos;


namespace Darwin.CronJobs.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<GetUserDto>> GetUsersAsync()
    {
        var response = await _httpClient.GetAsync("get");
        var users = await response.Content.ReadFromJsonAsync<DarwinResponse<List<GetUserDto>>>();
        return users.Data;
    }
}
