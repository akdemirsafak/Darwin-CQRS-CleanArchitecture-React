namespace Darwin.AuthServer.Models.Responses.Users;
public record GetUserResponse(string Id,

    string UserName,
    string Email, 
    string? Name,
    string? LastName,
    string? PhoneNumber,
    DateTime? BirthDate);