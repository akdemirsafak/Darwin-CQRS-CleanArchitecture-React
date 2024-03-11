using Darwin.Domain.Entities;
using Darwin.Domain.RequestModels.Users;
using Darwin.Domain.ResponseModels.Users;

namespace Darwin.Application.Services;

public interface IUserService
{
    Task DeleteAsync(string userId);
    Task SuspendUserAsync();
    Task<GetUserResponse> UpdateAsync(UpdateUserRequest updateUserRequest);
    Task<GetUserResponse> GetByIdAsync(string userId);
    Task<List<GetUserResponse>> GetAllAsync();
    Task ResetPasswordEmailVerify(string newPassword, string userId, string token);
    Task ConfirmEmailAsync(string userId, string token);
    Task<string> GenerateEmailConfirmationTokenAsyncByUserIdAsync(string id);
    Task<AppUser> FindByEmailAsync(string email);
    Task<AppUser> FindByIdAsync(string id);
    Task<string> GeneratePasswordResetTokenAsync(AppUser user);
}
