using Darwin.Application.Services;
using Darwin.Application.TokenOperations;
using Darwin.Domain.Dtos;
using Darwin.Domain.Entities;
using Darwin.Domain.RequestModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Darwin.Persistance.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<AppUser> RegisterAsync(RegisterRequest request)
    {

        AppUser appUser = new()
        {
            Email=request.Email,
            UserName=request.Email,
            SecurityStamp=Guid.NewGuid().ToString()
        };
        var registerResult = await _userManager.CreateAsync(appUser, request.Password);

        if (!registerResult.Succeeded)
            throw new Exception(registerResult.Errors.First().Description);

        var existRole= await _roleManager.RoleExistsAsync("User");
        if (!existRole) //Yoksa oluştur.
        {
            var createdRoleResult=await _roleManager.CreateAsync(new AppRole()
            {
                Name = "User",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
        }
        var addRoleAsync=await _userManager.AddToRoleAsync(appUser, "User");

        if (!addRoleAsync.Succeeded)
            throw new Exception("Rol atanamadı.");

        return appUser;
    }

    public async Task RevokeTokensAsync()
    {
        List<AppUser> users= await _userManager.Users.ToListAsync();

        users.ForEach(async user =>
        {
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        });
    }
    public async Task RevokeTokenByEmailAsync(string email)
    {
        var user= await _userManager.FindByEmailAsync(email);
        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        var existUser = await _userManager.FindByEmailAsync(request.Email);
        if (existUser is null)
            throw new Exception("User not found.");

        var loginResult = await _signInManager.CheckPasswordSignInAsync(existUser, request.Password, true);

        // User Locked
        if (loginResult.IsLockedOut)
            throw new AuthenticationException("User locked.Try again 5 min.");

        //Another Login Problems
        if (!loginResult.Succeeded)
            throw new AuthenticationException($"Failed to login {request.Email}");


        var token = await _tokenService.CreateTokenAsync(existUser);

        existUser.RefreshToken = token.RefreshToken;
        existUser.RefreshTokenExpiration = token.RefreshTokenExpiration;

        //Last Login işlemi yaptık.
        existUser.LastLogin = DateTime.UtcNow;

        var updateResult = await _userManager.UpdateAsync(existUser);

        await _userManager.UpdateSecurityStampAsync(existUser); // TODO !
        await _userManager.SetAuthenticationTokenAsync(existUser, "Default", "AccessToken", token.AccessToken);

        if (!updateResult.Succeeded)
            throw new Exception("Login cannot updated.");
        return token;
    }
}
