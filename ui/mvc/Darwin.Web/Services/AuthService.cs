using Darwin.Shared.Dtos;
using Darwin.Web.Models;
using Darwin.Web.ViewModels;

namespace Darwin.Web.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", loginDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Login failed");

        var content= await response.Content.ReadFromJsonAsync<DarwinResponse<TokenResponse>>();
        if(content.Errors is not null)
            throw new Exception(content.Errors.ToString());

        //Burada tokeni cookieye atıyoruz
        return true;

    }

    public Task<bool> Logout()
    {
        //Cookieyi siliyoruz
        return Task.FromResult(true);
    }

    public async Task<bool> Register(RegisterDto registerDto)
    {
        var response= await _httpClient.PostAsJsonAsync("auth/register", registerDto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Register failed");
        
        var content= await response.Content.ReadFromJsonAsync<DarwinResponse<UserViewModel>>();
        if(content.Errors is not null)
            throw new Exception(content.Errors.ToString());
        return true;
    }
}
