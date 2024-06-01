namespace Darwin.AuthServer.Requests.Users;
public record UpdateUserRequest(
    string Name,
    string LastName,
    string UserName,
    string PhoneNumber,
    DateTime? BirthDate
    );
