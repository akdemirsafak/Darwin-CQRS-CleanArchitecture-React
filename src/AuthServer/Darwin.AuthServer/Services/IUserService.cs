using Darwin.AuthServer.Models.Requests.Users;
using Darwin.AuthServer.Models.Responses.Users;
using Darwin.AuthServer.Requests.Users;
using Darwin.Shared.Dtos;

namespace Darwin.AuthServer.Services;

public interface IUserService
{
    Task<DarwinResponse<UpdatedUserResponse>> UpdateAsync(string userId, UpdateUserRequest updateUserRequest);
    Task<DarwinResponse<GetUserResponse>> GetByIdAsync(string userId);
    Task<DarwinResponse<NoContentDto>> DeleteAsync(string userId);
    //Task<DarwinResponse<List<GetUserResponse>>> GetAllAsync();
    Task<DarwinResponse<NoContentDto>> ChangePasswordAsync(ChangePasswordRequest request);

    //Task<DarwinResponse<NoContentDto>> SendConfirmationMail(string userId); // Kullanıcı profilden onaylama mesajı isterse.

    //
    Task<DarwinResponse<NoContentDto>> SendConfirmationEmailAsync(string userId);
    Task<DarwinResponse<NoContentDto>> ConfirmEmailAsync(string userId, string token);
    Task<string> GenerateEmailConfirmationTokenAsyncByUserIdAsync(string id);
    //

}
