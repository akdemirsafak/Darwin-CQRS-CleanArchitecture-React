namespace Darwin.AuthServer.Models.Responses.Users;
public record GetUserResponse(string Id,
    string Name,
    string LastName,
    string UserName,
    string Email,
    string PhoneNumber,
    DateTime? BirthDate);