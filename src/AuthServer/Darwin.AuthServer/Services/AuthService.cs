using Darwin.AuthServer.Controllers;
using Darwin.AuthServer.Entities;
using Darwin.AuthServer.Helper;
using Darwin.AuthServer.Mapper;
using Darwin.AuthServer.Models.Requests.Auth;
using Darwin.AuthServer.Models.Responses.Users;
using Darwin.Shared.Dtos;
using Darwin.Shared.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Darwin.AuthServer.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IUserService _userService;
    private readonly ILinkCreator _linkCreator;

    CommonMapper _mapper = new();


    public AuthService(UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        ISendEndpointProvider sendEndpointProvider,
        ILinkCreator linkCreator,
        IUserService userService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _sendEndpointProvider = sendEndpointProvider;
        _linkCreator = linkCreator;
        _userService = userService;
    }
    #region Register işlemi tamamlandı.
    public async Task<DarwinResponse<GetUserResponse>> RegisterAsync(RegisterRequest request)
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


        //Create confirmation link
        var confirmationToken = await _userService.GenerateEmailConfirmationTokenAsyncByUserIdAsync(appUser.Id);
        var confirmationUrl = await  _linkCreator.CreateTokenMailUrl("ConfirmEmail", "User", appUser.Id, confirmationToken);


        var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:user-created-event-queue"));

        var userCreatedEvent=new UserCreatedEvent
        {
            UserId=appUser.Id,
            Email = appUser.Email!,
            Name = appUser.Name,
            LastName = appUser.LastName,
            UserName = appUser.UserName!,
            PhoneNumber = appUser.PhoneNumber,
            EmailConfirmationLink=confirmationUrl
        };
        await sendEndpoint.Send<UserCreatedEvent>(userCreatedEvent);
        return DarwinResponse<GetUserResponse>.Success(_mapper.AppUserToGetUserResponse(appUser), 201);
    }
    #endregion



    public async Task<DarwinResponse<TokenResponse>> LoginAsync(LoginRequest request)
    {
        var existUser = await _userManager.FindByEmailAsync(request.Email);
        if (existUser is null)
            throw new AuthenticationException("User not found.");

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
        existUser.LastLogin = DateTime.Now;

        var updateResult = await _userManager.UpdateAsync(existUser);

        await _userManager.UpdateSecurityStampAsync(existUser); // TODO !
        await _userManager.SetAuthenticationTokenAsync(existUser, "Default", "AccessToken", token.AccessToken);

        if (!updateResult.Succeeded)
            throw new AuthenticationException("Login cannot updated.");

        return DarwinResponse<TokenResponse>.Success(token, 200);
    }

    public async Task<DarwinResponse<NoContentDto>> ForgotPasswordAsync(string email)
    {
        var user= await _userManager.FindByEmailAsync(email);
        if (user is null)
            return DarwinResponse<NoContentDto>.Fail("User not found.", 404);

        var token= await _userManager.GeneratePasswordResetTokenAsync(user);

        var resetLink=await _linkCreator.CreateTokenMailUrl("ResetPassword", nameof(UserController), user.Id, token); // Unutulan şifreyi yenilemek için.

        if(resetLink is null)
            return DarwinResponse<NoContentDto>.Fail("Reset link cannot created.", 500);

        var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:reset-password-event-queue"));

        var resetPasswordEvent=new ResetPasswordEvent
        {
            To=user.Email!,
            Url=resetLink
        };
        await sendEndpoint.Send<ResetPasswordEvent>(resetPasswordEvent);

        return DarwinResponse<NoContentDto>.Success(204);

    }

    public async Task<DarwinResponse<NoContentDto>> ResetPasswordAsync(string newPassword, string userId, string token)
    {
        AppUser? appUser= await _userManager.FindByIdAsync(userId);
        if (appUser is null)
            return DarwinResponse<NoContentDto>.Fail("User not found.", 404);

        var result=await _userManager.ResetPasswordAsync(appUser, token, newPassword);
        if (!result.Succeeded)
            return DarwinResponse<NoContentDto>.Fail(result.Errors.First().Description, 400);

        await _userManager.UpdateSecurityStampAsync(appUser);
        return DarwinResponse<NoContentDto>.Success(204);
    }

    public async Task<DarwinResponse<TokenResponse>> CreateAccessTokenByRefreshTokenAsync(CreateTokenByRefreshTokenRequest request)
    {
        var response= await _tokenService.CreateTokenByRefreshToken(request.RefreshToken, request.AccessToken);
        return DarwinResponse<TokenResponse>.Success(response, 200);
    }


    public async Task<DarwinResponse<NoContentDto>> RevokeTokensAsync()
    {
        List<AppUser> users= await _userManager.Users.ToListAsync();

        users.ForEach(async user =>
        {
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        });

        return DarwinResponse<NoContentDto>.Success(204);
    }
    public async Task<DarwinResponse<NoContentDto>> RevokeTokenByEmailAsync(string email) // Logout işlemi
    {
        var user= await _userManager.FindByEmailAsync(email);
        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);

        return DarwinResponse<NoContentDto>.Success(204);
    }
}
