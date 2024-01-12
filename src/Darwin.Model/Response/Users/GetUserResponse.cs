
namespace Darwin.Model.Response.Users;
public class GetUserResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public DateTime? BirthDate { get; set; }
}