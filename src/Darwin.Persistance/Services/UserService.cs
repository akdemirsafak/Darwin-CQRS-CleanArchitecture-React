using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Domain.RequestModels.Users;
using Darwin.Domain.ResponseModels.Users;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Persistance.Services;

public sealed class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICurrentUser _currentUser;

    public UserService(UserManager<AppUser> userManager, ICurrentUser currentUser)
    {
        _userManager = userManager;
        _currentUser = currentUser;
    }

    public async Task DeleteAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(_currentUser.GetUserId);
        var deleteResult = await _userManager.DeleteAsync(user);
    }

    public async Task<List<GetUserResponse>> GetAllAsync()
    {
        var users= await _userManager.Users.ToListAsync();
        return users.Adapt<List<GetUserResponse>>();
    }

    public async Task<GetUserResponse> GetByIdAsync(string userId)
    {
        var user=await _userManager.FindByIdAsync(userId);
        return user.Adapt<GetUserResponse>();
    }

    public async Task ResetPasswordEmailVerify(string newPassword, string userId, string token)
    {
        AppUser? user= await _userManager.FindByIdAsync(userId);
        var result=await _userManager.ResetPasswordAsync(user,token, newPassword);
        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);

        await _userManager.UpdateSecurityStampAsync(user);
    }

    public async Task SuspendUserAsync()
    {
        var user = await _userManager.FindByIdAsync(_currentUser.GetUserId);
        if (user is null)
            throw new Exception("User not found");

        user.IsActive = false;
        var response = await _userManager.UpdateAsync(user);

        if (response.Errors.Any())
            throw new Exception(response.Errors.Select(x => x.Description).ToList().First());

    }

    public async Task<GetUserResponse> UpdateAsync(UpdateUserRequest updateUserRequest)
    {
        AppUser? appUser = await _userManager.FindByIdAsync(_currentUser.GetUserId);

        if (appUser is null)
            throw new Exception("User not found");

        appUser.Name = updateUserRequest.Name;
        appUser.LastName = updateUserRequest.LastName;
        appUser.UserName = updateUserRequest.UserName;
        appUser.BirthDate = updateUserRequest.BirthDate;
        appUser.PhoneNumber = updateUserRequest.PhoneNumber;

        var updateResult = await _userManager.UpdateAsync(appUser);
        if (updateResult.Errors.Any())
            throw new Exception(updateResult.Errors.Select(x => x.Description).ToList().First());

        return appUser.Adapt<GetUserResponse>();
    }

    public async Task ConfirmEmailAsync(string userId, string token)
    {
        var existUser= await _userManager.FindByIdAsync(userId);
        var result=await _userManager.ConfirmEmailAsync(existUser!, token);
        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsyncByUserIdAsync(string id)
    {
        AppUser? user= await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new Exception("Kullanıcı bulunamadı.");
        var confirmationToken=await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return confirmationToken;
    }
    public async Task<AppUser> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
    public async Task<AppUser> FindByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }
}
