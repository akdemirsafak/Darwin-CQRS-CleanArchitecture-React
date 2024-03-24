
using System.ComponentModel.DataAnnotations;

namespace Darwin.Domain.RequestModels.Users;
public record UpdateUserRequest(
    [Required] string Name,
    string LastName,
    string UserName,
    [DataType(DataType.PhoneNumber)] string PhoneNumber,
    [DataType(DataType.Date)] DateTime? BirthDate
    );
