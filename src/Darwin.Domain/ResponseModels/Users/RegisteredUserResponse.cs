namespace Darwin.Domain.ResponseModels.Users;

public class RegisteredUserResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
}
