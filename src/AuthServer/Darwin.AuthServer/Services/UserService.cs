using Darwin.AuthServer.Entities;
using Darwin.AuthServer.Helper;
using Darwin.AuthServer.Mapper;
using Darwin.AuthServer.Models.Requests.Users;
using Darwin.AuthServer.Models.Responses.Users;
using Darwin.AuthServer.Requests.Users;
using Darwin.Shared.Dtos;
using Darwin.Shared.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Darwin.AuthServer.Services;

public sealed class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILinkCreator _linkCreator;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    CommonMapper _mapper=new CommonMapper();

    public UserService(UserManager<AppUser> userManager,
        ILinkCreator linkCreator,
        ISendEndpointProvider sendEndpointProvider)
    {
        _userManager = userManager;
        _linkCreator = linkCreator;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task<DarwinResponse<NoContentDto>> DeleteAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return DarwinResponse<NoContentDto>.Fail("Kullanıcı bulunamadı", 404);

        var result= await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
            return DarwinResponse<NoContentDto>.Fail("Kullanıcı silinirken bir hata oluştu", 500);
        return DarwinResponse<NoContentDto>.Success(204);
    }

    public async Task<DarwinResponse<GetUserResponse>> GetByIdAsync(string userId)
    {
        var user= await _userManager.FindByIdAsync(userId);
        if (user is null)
            return DarwinResponse<GetUserResponse>.Fail("Kullanıcı bulunamadı", 404);
        return DarwinResponse<GetUserResponse>.Success(_mapper.AppUserToGetUserResponse(user), 200);

    }

    public async Task<DarwinResponse<UpdatedUserResponse>> UpdateAsync(string userId, UpdateUserRequest updateUserRequest)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return DarwinResponse<UpdatedUserResponse>.Fail("Kullanıcı bulunamadı", 404);

        user.Name = updateUserRequest.Name;
        user.LastName = updateUserRequest.LastName;
        user.UserName = updateUserRequest.UserName;
        user.PhoneNumber = updateUserRequest.PhoneNumber;
        user.BirthDate = updateUserRequest.BirthDate;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return DarwinResponse<UpdatedUserResponse>.Fail("Kullanıcı güncellenirken bir hata oluştu", 500);

        return DarwinResponse<UpdatedUserResponse>.Success(_mapper.AppUserToUpdatedUserResponse(user), 200);

    }


    public async Task<DarwinResponse<NoContentDto>> ConfirmEmailAsync(string userId, string token)
    {
        var existUser= await _userManager.FindByIdAsync(userId);
        var result=await _userManager.ConfirmEmailAsync(existUser!, token);
        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);

        return DarwinResponse<NoContentDto>.Success(204);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsyncByUserIdAsync(string id)
    {
        AppUser? user= await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı.");
        var confirmationToken=await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return confirmationToken;
    }

    public async Task<DarwinResponse<NoContentDto>> ChangePasswordAsync(ChangePasswordRequest request)
    {
        var existUser= await _userManager.FindByEmailAsync(request.Email);
        if (existUser is null)
            throw new Exception("Kullanıcı bulunamadı.");

        var result= await _userManager.ChangePasswordAsync(existUser, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);

        return DarwinResponse<NoContentDto>.Success(204);

    }

    public async Task<DarwinResponse<NoContentDto>> SendConfirmationEmailAsync(string userId)
    {
        var user= await _userManager.FindByIdAsync(userId);
        if (user is null)
            return DarwinResponse<NoContentDto>.Fail("Kullanıcı bulunamadı", 404);

        var token= await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink= await _linkCreator.CreateTokenMailUrl("ConfirmEmail", "User", user.Id, token);

        var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:confirm-email-event-queue"));

        var confirmEmailEvent=new ConfirmEmailEvent
        {
            ConfirmationUrl=confirmationLink,
            To=user.Email!,
            UserName=user.UserName,
            FullName=user.Name+" "+user.LastName
        };

        await sendEndpoint.Send<ConfirmEmailEvent>(confirmEmailEvent);
        return DarwinResponse<NoContentDto>.Success(204);

    }

    public async Task<DarwinResponse<List<GetUserResponse>>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return DarwinResponse<List<GetUserResponse>>.Success(_mapper.AppUserListToGetUserResponseList(users), 200);
    }
}
